using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Api.Helpers;
using Lapka.Identity.Api.RequestWithValidation;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.RequestStorage;
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
    private readonly IUserRequestStorage _userRequestStorage;

    public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, 
        IUserRequestStorage userRequestStorage, IUserInfoProvider userInfoProvider) : base(userInfoProvider)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _userRequestStorage = userRequestStorage;
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
        var command = new LoginCommand(request.Email, request.Password);
        await _commandDispatcher.SendAsync(command);

        var tokens = new LoginResult(_userRequestStorage.GetToken(command.AccTokenCasheId),
            _userRequestStorage.GetToken(command.RefTokenCasheId));
        return Ok(tokens);
    }

    [HttpPost("useRefreshToken")]
    [SwaggerOperation(description: "Odnawia access token na podstawie refresh token")]
    [SwaggerResponse(204, Type = typeof(UseRefreshTokenResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UseRefreshToken([FromBody] UseRefreshTokenRequest request)
    {
        var command = new UseRefreshTokenCommand(request.AccessToken, request.RefreshToken);
        await _commandDispatcher.SendAsync(command);

        var token = new UseRefreshTokenResult(_userRequestStorage.GetToken(command.TokenCasheId));
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