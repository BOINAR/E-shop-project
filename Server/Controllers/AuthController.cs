using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.UserService;
using Server.Services.JwtTokenService;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(IUserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
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
        if (user == null)
        {
            return Unauthorized("Le Refresh Token est invalide ou expiré.");
        }

        // Générer de nouveaux tokens
        var newAccessToken = _jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        // Sauvegarder le nouveau refresh token
        await _userService.SaveRefreshToken(user, newRefreshToken);

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }

    [HttpPost("logout/{userId}")]
    public async Task<IActionResult> Logout([FromRoute] int userId)
    {
        Console.WriteLine($"Déconnexion de l'utilisateur {userId}");
        await _userService.LogoutAsync(userId);
        return NoContent();
    }
}