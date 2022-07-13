using Lapka.Identity.Application.DTO;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [SwaggerOperation(description: "Rejestracja użytkownika.")]
        [SwaggerResponse(204, Type = typeof(Guid))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Register([FromBody] UserRegDto userRegDto)
        {
            await _userService.Register(userRegDto);
            return Ok();
        }

        [HttpPost("login")]
        [SwaggerOperation(description: "Rejestracja użytkownika.")]
        //[SwaggerResponse(204, Type = typeof(Token))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Login([FromBody] UserLogDto dto)
        {
            // var token = _userService.Login(dto);
           //return Ok(new { Token = token });
           return Ok();
        }
    }
}
