using RuneSharper.Domain.Entities.Users;

namespace RuneSharper.Services.Services.Token;

public interface ITokenService
{
    string BuildToken(AppUser user);
}
