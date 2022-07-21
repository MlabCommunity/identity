using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Api.Helpers;
using Lapka.Identity.Api.RequestWithValidation;
using Lapka.Identity.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IUserInfoProvider userInfoProvider) : base(userInfoProvider)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("register")]
    [SwaggerOperation(description: "Rejestracja użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var command = new RegistrationCommand(request.Username, request.FirstName, request.LastName,
            request.EmailAddress, request.Password);

        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("login")]
    [SwaggerOperation(description: "Logowanie użytkownika - zwracanie tokenu.")]
    [SwaggerResponse(204, Type = typeof(LoginResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var tokens = await _queryDispatcher.QueryAsync(query);
        return Ok(tokens);
    }

    [HttpPost("useRefreshToken")]
    [SwaggerOperation(description: "Odnawia access token na podstawie refresh token")]
    [SwaggerResponse(204, Type = typeof(UseRefreshTokenResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UseRefreshToken([FromBody] UseRefreshTokenRequest request)
    {
        var query = new UseRefreshTokenQuery(request.AccessToken, request.RefreshToken);
        var token = await _queryDispatcher.QueryAsync(query);
        return Ok(token);
    }

    [Authorize]
    [HttpPost("revokeRefreshToken")]
    [SwaggerOperation(description: "Usuwa refresh token z bazy")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] TokenRequest request)
    {
        var command = new RevokeRefreshTokenCommand(GetUserId(), request.Token);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("resetPassword")]
    [SwaggerOperation(description: "Wysłanie maila z linkiem do zmiany hasła.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> ResetPassword([FromBody] UserEmailRequest request)
    {
        var command = new ResetPasswordCommand(request.Email);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("setNewPassword/{token}")]
    [SwaggerOperation(description: "Ustawienie nowego hasła.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> SetNewPassword([FromRoute] string token, [FromBody] SetNewPasswordRequest request)
    {
        var command = new SetNewPasswordCommand(token, request.Password, request.Email);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}