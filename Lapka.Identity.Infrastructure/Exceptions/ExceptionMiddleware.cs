using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lapka.Identity.Infrastructure.Exceptions;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ProjectException pex)
        {
            _logger.LogError(pex, pex.Message);

            var errorCode = (int)pex.ErrorCode;
            context.Response.StatusCode = errorCode;
            context.Response.Headers.Add("content-type", "application/json");

            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, pex.Message });
            await context.Response.WriteAsync(json);
        }
        catch (ValidationException vex)
        {
            _logger.LogError(vex, "validate error");
            var errorCode = 400;
            context.Response.StatusCode = errorCode;
            context.Response.Headers.Add("content-type", "application/json");

            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, vex.Message });
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "server error");

            var errorCode = 500;
            context.Response.StatusCode = errorCode;
            context.Response.Headers.Add("content-type", "application/json");

            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
            await context.Response.WriteAsync(json);
        }
    }
}
