using Lappka.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddHostedService<AppInitializer>();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(configuration);
//TODO: add application


var app = builder.Build();

app.UseHttpsRedirection();

//app.UseMiddleware();

app.UseAuthentication();
app.MapControllers();

app.Run();