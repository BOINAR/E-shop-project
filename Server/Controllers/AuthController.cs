using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.UserService;
using Server.Services.JwtTokenService;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtTokenService _JwtTokenService;

    public AuthController(UserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _JwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Les informations de connexion sont manquantes.");
        }

        // Appel à la méthode LoginAsync qui connecte et Authentifie l'utilisateur
        var user = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return Unauthorized("Email ou mot de passe incorrect.");
        }

        // Générer l'access token
        var token = _JwtTokenService.GenerateAccessToken(user);

        // Retourner le token dans la réponse
        return Ok(new { Token = token });
    }
}