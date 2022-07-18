using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserInfoProvider _userInfoProvider;

    public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IUserInfoProvider userInfoProvider)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _userInfoProvider = userInfoProvider;
    }

    [HttpPost("register")]
    [SwaggerOperation(description: "Rejestracja użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Register([FromBody] UserRegCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("login")]
    [SwaggerOperation(description: "Logowanie użytkownika - zwracanie tokenu.")]
    [SwaggerResponse(204, Type = typeof(UserLogResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Login([FromBody] UserLogQuery query)
    {
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [Authorize]
    [HttpGet("testAuth")]
    public async Task<IActionResult> TestAuth()
    {
        var username = _userInfoProvider.Name;
        return Ok($"Witam pozdrawiam {username}");
    }
}