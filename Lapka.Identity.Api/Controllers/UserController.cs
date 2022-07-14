using Lapka.Identity.Application.Commands;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthService _userService;

        public UserController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [SwaggerOperation(description: "Rejestracja użytkownika.")]
        [SwaggerResponse(204, Type = typeof(Guid))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Register([FromBody] UserRegCommand userRegDto)
        {
            var id = await _userService.Register(userRegDto);
            return Ok(new {Id = id});
        }

        [HttpPost("login")]
        [SwaggerOperation(description: "Rejestracja użytkownika.")]
        //[SwaggerResponse(204, Type = typeof(Token))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Login([FromBody] UserLogCommand dto)
        {
            // var token = _userService.Login(dto);
           //return Ok(new { Token = token });
           return Ok();
        }
    }
}
