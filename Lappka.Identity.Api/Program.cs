using System.Text;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Application;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddMiddleware();
//TODO: migrate this to application

//JWT
builder.Services.Configure<JwtSettings>(configuration.GetSection("jwt"));
builder.Services.AddSingleton<IJwtHandler, JwtHandler>();


ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
var jwtHandler = serviceProvider.GetService<IJwtHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = jwtHandler.Parameters;
    });
    
builder.Services.AddControllers();

builder.Services.AddConvey()
    .AddCommandHandlers()
    .AddInMemoryCommandDispatcher()
    .AddQueryHandlers()
    .AddInMemoryQueryDispatcher()
    .Build();


builder.Services.AddInfrastructure(configuration);

//TODO: add application


//TODO: migrate this somewhere else
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    
    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
   
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

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