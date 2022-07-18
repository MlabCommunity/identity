using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Application.Queries;
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
    private readonly IUserInfoProvider _userInfoProvider;

    public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IUserInfoProvider userInfoProvider)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _userInfoProvider = userInfoProvider;
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation(description: "Informacje o użytkowniku.")]
    [SwaggerResponse(204, Type = typeof(GetUserDataQueryResult))]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetUser()
    {
        var query = new GetUserDataQuery();
        var userData = await _queryDispatcher.QueryAsync(query);
        return Ok(userData);
    }
}