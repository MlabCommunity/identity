using Lapka.Identity.Api.Extensions;
using Lapka.Identity.Application;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(c =>
    {
        c.SignIn.RequireConfirmedPhoneNumber = false;
        c.SignIn.RequireConfirmedEmail = false;
        c.User.RequireUniqueEmail = true;
    })
.AddEntityFrameworkStores<DataContext>();

var connectionString = builder.Configuration.GetConnectionString("postgres");
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerA();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseInfrastucture();

app.MapControllers();

app.Run();
