using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Api.Requests;
using Lappka.Identity.Application.Auth.Commands;
using Lappka.Identity.Application.Auth.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lappka.Identity.Api.Controllers;

public class AuthController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("register")]
    [SwaggerOperation(description: "Register a new user")]
    [SwaggerResponse(200, "If user was registered successfully")]
    [SwaggerResponse(400, "if credentials are invalid")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
    {
        var command = new RegistrationCommand
        {
            Email = request.Email,
            Username = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("login")]
    [SwaggerOperation(description: "Login a user")]
    [SwaggerResponse(200, "If user was logged in successfully")]
    [SwaggerResponse(404, "If user was not found")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        await _commandDispatcher.SendAsync(command);

        var query = new GetTokensQuery
        {
            Email = request.Email,
        };

        var tokens = await _queryDispatcher.QueryAsync(query);
        return Ok(tokens);
    }

    [HttpPost("use")]
    [SwaggerOperation(description: "Generates a new token")]
    [SwaggerResponse(200, "If token was generated successfully")]
    [SwaggerResponse(404, "If user or token was not found")]
    public async Task<IActionResult> UseTokensAsync([FromBody] UseTokenRequest request)
    {
        var query = new UseTokenQuery
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };
        var tokens = await _queryDispatcher.QueryAsync(query);
        return Ok(tokens);
    }

    [HttpPost("revoke")]
    [Authorize]
    [SwaggerOperation(description: "Revokes a token, user must be logged in")]
    [SwaggerResponse(200, "If token was revoked successfully")]
    [SwaggerResponse(404, "If user or token was not found")]
    [SwaggerResponse(401, "if user is not logged in or token expired")]
    public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequest request)
    {
        var command = new RevokeTokenCommand
        {
            UserId = GetPrincipalId(),
            RefreshToken = request.RefreshToken
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}