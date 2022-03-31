using RuneSharper.Shared.Entities;

namespace RuneSharper.Services.Token
{
    public interface ITokenService
    {
        string BuildToken(AppUser user);
    }
}
