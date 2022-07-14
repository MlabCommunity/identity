using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;


//TODO: migrate this to application


builder.Services.Configure<JwtSettings>(configuration.GetSection("jwt"));
builder.Services.AddSingleton<IJwtHandler,JwtHandler>();



builder.Services.AddControllers();

builder.Services.AddConvey()
    .AddCommandHandlers()
    .AddInMemoryCommandDispatcher()
    .AddQueryHandlers()
    .AddInMemoryQueryDispatcher()
    .Build();

builder.Services.AddInfrastructure(configuration);

//TODO: add application


var app = builder.Build();

app.UseConvey();


var jwtHandler = app.ApplicationServices.GetService<IJwtHandler>();
app.UseJwtBearerAuthentication(new JwtBearerOptions
{
    AutomaticAuthenticate = true,
    TokenValidationParameters = jwtHandler.Parameters
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();