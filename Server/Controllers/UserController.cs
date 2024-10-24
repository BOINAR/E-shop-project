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
        public ActionResult<User> Register([FromBody] User newUser)
        {
            var user = _userService.Register(newUser);
            return Ok(user);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var UserLogin = _userService.Login(loginRequest.Email, loginRequest.Password);

            return Ok(UserLogin);
        }
        [HttpPost("logout")]
        public IActionResult Logout(int userId)
        {
            // Logique pour déconnecter l'utilisateur
            _userService.Logout(userId);
            return Ok(); // Retourne une réponse 200 OK
        }

    }
}