using Microsoft.AspNetCore.Identity;

namespace RuneSharper.Shared.Entities.Users;

public class AppUser : IdentityUser<int>
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public override string? UserName { get => Email; set => Email = value; }
    public override string? NormalizedUserName { get => NormalizedEmail; set => NormalizedEmail = value; }
}
