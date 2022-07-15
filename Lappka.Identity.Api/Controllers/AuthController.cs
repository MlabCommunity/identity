using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Commands;
using Lappka.Identity.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lappka.Identity.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController :ControllerBase
{

    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    
    public AuthController(ICommandDispatcher commandDispatcher,IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginQuery query)
    {
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }
    
    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshTokensAsync(RefreshTokensQuery query)
    {
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Hello()
    {
        return Ok(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
    
}