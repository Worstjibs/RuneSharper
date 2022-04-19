using RuneSharper.Shared.Entities.Users;

namespace RuneSharper.Services.Token
{
    public interface ITokenService
    {
        string BuildToken(AppUser user);
    }
}
