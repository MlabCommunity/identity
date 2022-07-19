using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("register")]
    [SwaggerOperation(description: "Rejestracja użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Register([FromBody] RegistrationCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("login")]
    [SwaggerOperation(description: "Logowanie użytkownika - zwracanie tokenu.")]
    [SwaggerResponse(204, Type = typeof(LoginResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var tokens = await _queryDispatcher.QueryAsync(query);
        return Ok(tokens);
    }

    [HttpPost("useRefreshToken")]
    [SwaggerOperation(description: "Odnawia access token na podstawie refresh token")]
    [SwaggerResponse(204, Type = typeof(UseRefreshTokenResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UseRefreshToken([FromBody] UseRefreshTokenQuery query)
    {
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [Authorize]
    [HttpPost("revokeRefreshToken")]
    [SwaggerOperation(description: "Usuwa refresh token z bazy")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}