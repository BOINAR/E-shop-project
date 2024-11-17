using Server.Data;
using Server.Repositories;
using Server.Repositories.CartItemRepository;
using Server.Repositories.CartRepository;
using Server.Repositories.CategoryRepository;
using Server.Repositories.OrderItemRepository;
using Server.Repositories.OrderRepository;
using Server.Repositories.ProductRepository;
using Server.Repositories.UserRepository;
using Server.Services.CartItemService;
using Server.Services.CartService;
using Server.Services.CategoryService;
using Server.Services.OrderItemService;
using Server.Services.OrderService;
using Server.Services.ProductService;
using Server.Services.UserService;
using Server.Services.JwtTokenService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Certificate;
using Server.Services.RefreshTokenServices;


// Charger les variables d'environnement à partir d'un fichier .env si nécessaire
Env.Load();


// Ajoute les services d'autorisation

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Vérifier la présence des clés essentielles dans la configuration
var secretKeyString = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

if (string.IsNullOrEmpty(secretKeyString))
{
    throw new InvalidOperationException("La clé secrète JWT n'est pas définie.");
}
var secretKey = Encoding.UTF8.GetBytes(secretKeyString);



// Configure les contrôleurs
builder.Services.AddControllers();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

// Ajoute les services d'autorisation
builder.Services.AddAuthorization();






// Construction de la chaîne de connexion à partir des variables d'environnement
var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                       $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

// Configure DbContext avec la connexion à la base de données
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseNpgsql(connectionString));

Console.WriteLine($"Host: {Environment.GetEnvironmentVariable("POSTGRES_HOST")}");
Console.WriteLine($"Port: {Environment.GetEnvironmentVariable("POSTGRES_PORT")}");
Console.WriteLine($"Database: {Environment.GetEnvironmentVariable("POSTGRES_DB")}");
Console.WriteLine($"Username: {Environment.GetEnvironmentVariable("POSTGRES_USER")}");
Console.WriteLine($"Password: {Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");

// Enregistrement des repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();


// Enregistrement des services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<RefreshTokenService>();

// Ajouter les services nécessaires
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});





var app = builder.Build();

// Test de la connexion à la base de données
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    try
    {
        // Vérifie la connexion à la base de données
        dbContext.Database.CanConnect();
        Console.WriteLine("Connexion à la base de données réussie.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Échec de la connexion à la base de données: {ex.Message}");
    }
}

// Enregistrer le port d'écoute dans les logs au démarrage
var port = app.Urls?.FirstOrDefault() ?? "Port inconnu";
app.Logger.LogInformation($"Application démarrée sur : {port}");



// Configuration des middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Remplacez par votre page d'erreur
    app.UseHsts();
}

// app.UseHttpsRedirection();    // Redirection vers HTTPS
app.UseStaticFiles();         // Pour servir les fichiers statiques (comme CSS, JS, images)

app.UseRouting();             // Active le routage

// Active les middlewares d'authentification et d'autorisation
app.UseAuthentication();
app.UseAuthorization();  // Gère l'autorisation après l'authentification

// Applique la politique CORS à toutes les requêtes
app.UseCors("AllowAll");

// Mappage des contrôleurs
app.MapControllers();

// Détection et affichage des URLs
app.Lifetime.ApplicationStarted.Register(() =>
{
    var addresses = app.Urls;
    foreach (var address in addresses)
    {
        app.Logger.LogInformation($"Application démarrée et écoute sur : {address}");
    }
});



// Lancer l'application
app.Run();












