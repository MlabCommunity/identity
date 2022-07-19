using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Api.Requests;
using Lappka.Identity.Application.User.Commands;
using Lappka.Identity.Application.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lappka.Identity.Api.Controllers;

[ApiController]
[Route("api/identity/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQuery()
        {
            UserId = id
        };
        var user = await _queryDispatcher.QueryAsync(query);
        return Ok(user);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = new UpdateUserCommand
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("email")]
    public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailRequest request)
    {
        var command = new UpdateUserEmailCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = request.Email
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("password/confirm")]
    [Authorize]
    public async Task<IActionResult> ConfirmUserPassword(ConfirmUpdateUserPasswordRequest request)
    {
        var command = new ConfirmUpdateUserPasswordCommand()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = request.Email,
            ConfirmationToken = request.ConfirmationToken,
            Password = request.Password,
            ConfirmPassword = request.ConfiremedPassword
        };

        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPatch("password")]
    [Authorize]
    public async Task<IActionResult> UpdateUserPassword()
    {
        var query = new UpdateUserPasswordQuery
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
        var confirmationToken = await _queryDispatcher.QueryAsync(query);
        return Ok(confirmationToken);
    }
}