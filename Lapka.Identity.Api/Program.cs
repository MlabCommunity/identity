using System.Reflection;
using FluentValidation.AspNetCore;
using Lapka.Identity.Api.Extensions;
using Lapka.Identity.Api.Helpers;
using Lapka.Identity.Application;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure;
using Lapka.Identity.Infrastructure.DataBase;
using Lapka.Identity.Infrastructure.gRPC;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserInfoProvider, UserInfoProvider>();

builder.Services.AddIdentity<AppUser, AppRole>(c =>
    {
        c.SignIn.RequireConfirmedPhoneNumber = false;
        c.SignIn.RequireConfirmedEmail = false;
        c.User.RequireUniqueEmail = true;
    })
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(1));

var GrpcOption = builder.Configuration.GetOptions<GrpcSettings>("gRPC");
builder.Services.AddSingleton(GrpcOption);

var connectionString = builder.Configuration.GetConnectionString("postgres");
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerA();

builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

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