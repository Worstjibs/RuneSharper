using RuneSharper.Domain.Entities.Users;

namespace RuneSharper.Application.Services.Token;

public interface ITokenService
{
    string BuildToken(AppUser user);
}
