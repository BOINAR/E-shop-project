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
        public async Task<ActionResult<User>> Register([FromForm] User newUser, [FromForm] string password)
        {
            try
            {
                // Appeler la méthode du service pour enregistrer l'utilisateur
                var user = await _userService.RegisterAsync(newUser, password);

                // Retourner une réponse OK avec l'utilisateur créé
                return Ok(user);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner une BadRequest ou autre selon le type d'erreur
                return BadRequest(ex.Message);
            }

        }

        // Action pour mettre à jour les informations de l'utilisateur
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromForm] User updatedUser)
        {
            // Appeler le service pour effectuer la mise à jour
            var user = await _userService.UpdateUser(userId, updatedUser);

            if (user == null)
            {
                return NotFound("Utilisateur introuvable ou non valide.");
            }

            return Ok(user);
        }





    }
}