using System.Security.Claims;
using Lappka.Identity.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Api.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected Guid GetPrincipalId()
    {
        var stringId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (stringId.IsNullOrEmpty())
        {
            throw new UserNotFoundException();
        }

        return Guid.Parse(stringId);
    }
}