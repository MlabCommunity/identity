using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Postgres.Repositories;

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
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser> FindByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IdentityResult> RegisterAsync(AppUser user, UserExtended userExtended, string password)
    {
        user.UserExtended = userExtended;
        await _context.Users.AddAsync(user);
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> LoginAsync(AppUser user, string password)
        => await _signInManager.CheckPasswordSignInAsync(user, password, false);


    public async Task ResetPasswordAsync(AppUser user, string confirmationToken, string password)
        => await _userManager.ResetPasswordAsync(user, confirmationToken, password);


    public Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => _userManager.GeneratePasswordResetTokenAsync(user);
}