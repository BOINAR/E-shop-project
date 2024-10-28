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
        public async Task<ActionResult<User>> Register([FromBody] User newUser)
        {
            var user = await _userService.RegisterAsync(newUser);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] User loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var UserLogin = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);

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