using Lappka.Identity.Core.Consts;

namespace Lappka.Identity.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public User()
    {
    }
    
    public User(string userName, string email, string password, Role role)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        Password = password;
        Role = role;
    }
}