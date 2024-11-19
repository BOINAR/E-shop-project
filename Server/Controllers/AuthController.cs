using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.UserService;
using Server.Services.JwtTokenService;
using Server.Services.RefreshTokenServices;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JWTtokenService _jwtTokenService;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly ILogger<RefreshTokenRequest> _logger;

    public AuthController(IUserService userService, JWTtokenService jwtTokenService, RefreshTokenService refreshTokenService, ILogger<RefreshTokenRequest> logger)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Les informations de connexion sont manquantes.");
        }

        var user = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return Unauthorized("Email ou mot de passe incorrect.");
        }

        // Révoquer l'ancien refresh token s'il existe

        await _refreshTokenService.RevokeRefreshToken(user.Id);


        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        await _userService.SaveRefreshToken(user, refreshToken);

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return BadRequest("Le Refresh Token est requis.");
        }

        // Valider le token et récupérer l'utilisateur
        var user = await _userService.ValidateRefreshTokenAsync(request.RefreshToken);
        try
        {
            if (user == null)
            {
                return Unauthorized("Le Refresh Token est invalide ou expiré.");
            }

            // Révoquer l'ancien refresh token
            await _refreshTokenService.RevokeRefreshToken(user.Id);

            // Générer de nouveaux tokens
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            // Sauvegarder le nouveau refresh token
            await _userService.SaveRefreshToken(user, newRefreshToken);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        catch (Exception ex)
        {
            var userIdInfo = user!.Id!.ToString();
            _logger.LogError($"Échec de la révocation des Refresh Tokens pour l'utilisateur {user.Id}.", ex);
            return StatusCode(500, "Erreur interne.");
        }
    }


    [HttpPost("logout/{userId}")]
    public async Task<IActionResult> Logout([FromRoute] int userId)
    {
        Console.WriteLine($"Déconnexion de l'utilisateur {userId}");
        await _userService.LogoutAsync(userId);
        return NoContent();
    }
}