using Lapka.Identity.Api.Helpers;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Lapka.Identity.Api.Controllers;

public abstract class BaseController : Controller
{
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly Guid _userId;

    protected BaseController(IUserInfoProvider userInfoProvider)
    {
        _userInfoProvider = userInfoProvider;
        _userId = _userInfoProvider.Id;
    }

    protected Guid GetUserId()
    {
        if (_userId == Guid.Empty)
        {
            throw new UserNotFoundException();
        }
        return _userId;
    }
}

