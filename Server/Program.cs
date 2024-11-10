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
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Certificate;



// Ajoute les services d'autorisation

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

// Ajoute les services d'autorisation
builder.Services.AddAuthorization();


builder.Services.AddControllers();

// Charger les variables d'environnement à partir d'un fichier .env si nécessaire
Env.Load();

// Construction de la chaîne de connexion à partir des variables d'environnement
var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                       $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                       $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

// Configure DbContext avec la connexion à la base de données
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseNpgsql(connectionString));

Console.WriteLine($"Host: {Environment.GetEnvironmentVariable("POSTGRES_HOST")}");
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

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
// Active l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();



// Mappage des contrôleurs

app.MapControllers(); // Si vous utilisez des contrôleurs



app.Run();












