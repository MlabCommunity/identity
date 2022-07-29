using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Api.Requests;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.User.Commands;
using Lappka.Identity.Application.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lappka.Identity.Api.Controllers;

[Authorize]
public class UserController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(description: "Gets user by id")]
    [SwaggerResponse(200, "User found", typeof(UserDto))]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var query = new GetUserByIdQuery()
        {
            UserId = id
        };
        var user = await _queryDispatcher.QueryAsync(query);
        return Ok(user);
    }

    [HttpPatch]
    [SwaggerOperation(description: "Updates principal, user must be logged in")]
    [SwaggerResponse(200, "User updated")]
    [SwaggerResponse(404, "User not found")]
    [SwaggerResponse(400, "If credentials are invalid")]
    [SwaggerResponse(401, "If user is not logged in or token expired")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = new UpdateUserCommand
        {
            Id = GetPrincipalId(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("email")]
    [SwaggerOperation(description: "Generates new email confirmation token")]
    [SwaggerResponse(200, "If user updated successfully")]
    [SwaggerResponse(400, "If credentials are invalid")]
    [SwaggerResponse(401, "If user is not logged in or token expired")]
    public async Task<IActionResult> ChangeUserEmail()
    {
        var query = new ChangeEmailQuery
        {
            UserId = GetPrincipalId()
        };
        var confirmationToken = await _queryDispatcher.QueryAsync(query);
        return Ok(confirmationToken);
    }

    [HttpPatch("email/confirm")]
    [SwaggerOperation(description: "Updates principal user email")]
    [SwaggerResponse(200, "If user updated successfully")]
    [SwaggerResponse(400, "If credentials are invalid")]
    [SwaggerResponse(401, "If user is not logged in or token expired")]
    public async Task<IActionResult> ConfirmChangeEmail(UpdateEmailRequest request)
    {
        var command = new ConfirmChangeEmailCommand
        {
            UserId = GetPrincipalId(),
            Email = request.Email,
            ConfirmationToken = request.ConfirmationToken,
            Password = request.Password
            
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("password/confirm")]
    [AllowAnonymous]
    [SwaggerOperation(description: "Confirms token with user data to reset password")]
    [SwaggerResponse(200, "If token confirmed successfully")]
    [SwaggerResponse(401, "If user is not logged in or token expired")]
    public async Task<IActionResult> ConfirmPassword(ConfirmPasswordRequest request)
    {
        var command = new ConfirmResetPasswordCommand()
        {
            Email = request.Email,
            ConfirmationToken = request.ConfirmationToken,
            Password = request.Password,
            ConfirmPassword = request.ConfirmedPassword
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("password")]
    [SwaggerOperation(description: "Generates token to reset password")]
    [SwaggerResponse(200, "If token generated successfully", typeof(string))]
    [SwaggerResponse(401, "If user is not logged in or token expired")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var query = new ResetPasswordQuery
        {
            Email = request.Email
        };
        var confirmationToken = await _queryDispatcher.QueryAsync(query);
        return Ok(confirmationToken);
    }
}