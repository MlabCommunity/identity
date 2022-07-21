using Lappka.Identity.Core.Consts;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Database.Postgres.Repositories;

internal class PostgresUserRepository : IUserRepository
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public PostgresUserRepository(AppDbContext context, UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<AppUser> FindByIdAsync(Guid userId)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser> FindByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IdentityResult> RegisterAsync(AppUser user, UserExtended userExtended, string password, Role role)
    {
        user.UserExtended = userExtended;
        var createResult = await _userManager.CreateAsync(user, password);
        var roleResult = await _userManager.AddToRoleAsync(user, role.ToString());

        return createResult.Succeeded && roleResult.Succeeded
            ? IdentityResult.Success
            : IdentityResult.Failed(createResult.Errors.Concat(roleResult.Errors).ToArray());
    }

    public async Task<SignInResult> LoginAsync(AppUser user, string password)
        => await _signInManager.CheckPasswordSignInAsync(user, password, false);


    public async Task ResetPasswordAsync(AppUser user, string confirmationToken, string password)
        => await _userManager.ResetPasswordAsync(user, confirmationToken, password);


    public Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => _userManager.GeneratePasswordResetTokenAsync(user);

    public Task AddToRoleAsync(AppUser user, Role role)
        => _userManager.AddToRoleAsync(user, role.ToString());
}