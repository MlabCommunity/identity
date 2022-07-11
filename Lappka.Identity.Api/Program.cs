using Lappka.Identity.Infrastructure;
using Lappka.Identity.Shared;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddShared();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(configuration);
//TODO: add application


var app = builder.Build();

app.UseHttpsRedirection();

app.UseShared();

app.UseAuthorization();

app.MapControllers();

app.Run();