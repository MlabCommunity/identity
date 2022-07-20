using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Lappka.Identity.Application.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ProjectException ex)
        {

            context.Response.StatusCode = ex._errorCode;
            context.Response.Headers.Add("content-type", "application/json");
            
            var json = JsonSerializer.Serialize(new {ErrorCode = ex._errorCode, ex.Message,Errors=ex.ExceptionData});
            await context.Response.WriteAsync(json);
        }
    }
    
}