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

    public PostgresUserRepository(AppDbContext context, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AppUser> FindByIdAsync(Guid userId)
        => await _context.Users.Include(x=>x.UserExtended).FirstOrDefaultAsync(u => u.Id == userId);

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser> FindByEmailAsync(string email)
        => await _context.Users.Include(x=>x.UserExtended).FirstOrDefaultAsync(u => u.Email == email);


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


    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string confirmationToken, string password)
        => await _userManager.ResetPasswordAsync(user, confirmationToken, password);

    public async Task<IdentityResult> ChangeEmailAsync(AppUser user, string confirmationToken, string email)
        => await _userManager.ChangeEmailAsync(user, email, confirmationToken);


    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => await _userManager.GeneratePasswordResetTokenAsync(user);

    public async Task<string> GenerateEmailResetTokenAsync(AppUser user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);

    public async Task AddToRoleAsync(AppUser user, Role role)
        => await _userManager.AddToRoleAsync(user, role.ToString());

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);
}