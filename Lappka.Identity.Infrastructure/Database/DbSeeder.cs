using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Database.Context;
using Microsoft.AspNetCore.Identity;
using static Lappka.Identity.Core.Consts.Role;

namespace Lappka.Identity.Infrastructure.Database;

public class DbSeeder
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public DbSeeder(AppDbContext context, IUserRepository userRepository, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _userRepository = userRepository;
        _roleManager = roleManager;
    }

    public DbSeeder(AppDbContext context)
    {
        _context = context;
    }
    
    public async void Seed()
    {
        if (!_context.Roles.Any())
        {
            var adminRole = new IdentityRole<Guid> { Name = ADMIN.ToString() };
            var userRole = new IdentityRole<Guid> { Name = USER.ToString() };
            var organizationRole = new IdentityRole<Guid> { Name = ORGANIZATION.ToString() };

            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(organizationRole);
        }

        if (!_context.Users.Any())
        {
            var user = new AppUser { UserName = "Admin", Email = "admin@example.com" };
            var userExtended = new UserExtended("AdminFirstName", "AdminLastName");
            await _userRepository.RegisterAsync(user, userExtended, "Admin123!", ADMIN);
        }
    }
}