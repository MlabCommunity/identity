using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Application.Queries;
using Lapka.Identity.Application.RequestWithValidation;
using Lapka.Identity.Application.Validation.RequestWithValidation;
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

    public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation(description: "Informacje o zalogowanym użytkowniku.")]
    [SwaggerResponse(204, Type = typeof(GetUserDataQueryResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetUser()
    {
        var query = new GetUserDataQuery();
        var userData = await _queryDispatcher.QueryAsync(query);
        return Ok(userData);
    }

    [Authorize]
    [HttpGet("{id}")]
    [SwaggerOperation(description: "Informacje o użytkowniku o podanym id.")]
    [SwaggerResponse(204, Type = typeof(GetUserDataQueryResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var query = new GetUserDataByIdQuery(id);
        var userData = await _queryDispatcher.QueryAsync(query);
        return Ok(userData);
    }

    [Authorize]
    [HttpPatch]
    [SwaggerOperation(description: "Aktualizuj Informacje o zalogowanym użytkowniku.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataRequest request)
    {
        var command = new UpdateUserDataCommand(request.Username, request.FirstName, request.LastName);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("Password")]
    [SwaggerOperation(description: "Aktualizuj hasło zalogowanego użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UserPasswordRequest request)
    {
        var command = new UpdateUserPasswordCommand(request.Password);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("Email")]
    [SwaggerOperation(description: "Aktualizuj email zalogowanego użytkownika.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest request)
    {
        var command = new UpdateUserEmailCommand(request.Email);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}