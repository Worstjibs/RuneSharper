using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RuneSharper.Domain.Entities.Users;
using RuneSharper.Shared.Settings;

namespace RuneSharper.Services.Services.Token;

public class TokenService : ITokenService
{
    private readonly JwtTokenSettings _jwtSettings;

    public TokenService(IOptions<JwtTokenSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string BuildToken(AppUser user)
    {
        var claims = new Claim[] {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}
