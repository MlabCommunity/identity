using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using FluentValidation.AspNetCore;
using Lappka.Identity.Api.Extensions;
using Lappka.Identity.Application;
using Lappka.Identity.Infrastructure;
using Lappka.Identity.Infrastructure.Database;
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
    .AddEventHandlers()
    .AddInMemoryEventDispatcher()
    .AddRabbitMq()
    .Build();

builder.Services.AddAuth(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerA();

builder.Services.AddScoped<DbSeeder>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>(); 
seeder.Seed();

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
