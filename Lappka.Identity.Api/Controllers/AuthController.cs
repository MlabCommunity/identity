using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Api.Requests;
using Lappka.Identity.Application.Auth.Commands;
using Lappka.Identity.Application.Auth.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lappka.Identity.Api.Controllers;

[ApiController]
[Route("api/identity/auth")]
public class AuthController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
    {
        var command = new RegistrationCommand()
        {
            Email = request.Email,
            Username = request.Username,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var query = new LoginQuery
        {
            Email = request.Email,
            Password = request.Password
        };

        var tokens = await _queryDispatcher.QueryAsync(query);
        return Ok(tokens);
    }

    [HttpPost("use")]
    public async Task<IActionResult> UseTokensAsync([FromBody] UseTokenRequest request)
    {
        var query = new UseTokenQuery
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequest request)
    {
        var command = new RevokeTokenCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            RefreshToken = request.RefreshToken
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}