using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using FluentValidation.AspNetCore;
using Lappka.Identity.Api.Extensions;
using Lappka.Identity.Application;
using Lappka.Identity.Infrastructure;
using Lappka.Identity.Infrastructure.Exceptions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddMiddleware();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services
    .AddControllers(o => o.Filters.Add<ValidationFilter>())
    .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddCredentialsConfig();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddConvey()
    .AddCommandHandlers()
    .AddInMemoryCommandDispatcher()
    .AddQueryHandlers()
    .AddInMemoryQueryDispatcher()
    .Build();

builder.Services.AddAuth(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerA();

var app = builder.Build();

app.UseConvey();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();