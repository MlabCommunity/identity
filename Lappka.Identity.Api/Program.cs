using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Api.Extensions;
using Lappka.Identity.Application;
using Lappka.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddMiddleware();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();
builder.Services.AddCredentialsConfig();
    
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