using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IAuthService _authService;

    public UserController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [SwaggerOperation(description: "Rejestracja użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Register([FromBody] UserRegCommand command)
    {
        await _authService.RegisterUser(command);
        return NoContent();
    }

    /*
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
    */
}