using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.UserService;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromForm] User newUser, string password)
        {
            var user = await _userService.RegisterAsync(newUser, password);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromForm] User loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.PasswordHash))
            {
                return BadRequest("Email and password are required.");
            }
            var UserLogin = await _userService.LoginAsync(loginRequest.Email, loginRequest.PasswordHash);

            return Ok(UserLogin);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(int userId)
        {
            // Logique pour déconnecter l'utilisateur
            await _userService.LogoutAsync(userId);
            return Ok(); // Retourne une réponse 200 OK
        }

    }
}