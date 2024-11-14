using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaScarletteGalo.DTos;
using PruebaTecnicaScarletteGalo.Services;


namespace PruebaTecnicaScarletteGalo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) { 
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO model)
        {
            try
            {
                var user = _userService.Register(model.Username, model.Password, model.Role);
                return Ok(user);
            } catch (InvalidOperationException ex )
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDTO model)
        {
            try
            {
                var (user, token) = _userService.Authenticate(model.Username, model.Password);
                return Ok(new
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Role = user.Role,
                    Token = token
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(Guid userId)
        {
            var user = _userService.GetUserByID(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers().ToList();
            return Ok(users);
        }
    }
}