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
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginQuery query)
    {
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [HttpPost("recreate")]
    [Authorize]
    public async Task<IActionResult> RefreshTokensAsync([FromBody] RecreateTokenRequest request)
    {
        var query = new RecreateTokenQuery
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Hello()
    {
        return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}