using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Domain.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extensions;
public class TokenExtensions
{
    public static (string, DateTime) GenerateAccessToken(TokenConfiguration configuration, IEnumerable<Claim> claims)
    {
        var key = Encoding.UTF8.GetBytes(configuration.Secret);

        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var expire = DateTime.UtcNow.AddMinutes(configuration.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken(
            issuer: configuration.Issuer,
            audience: configuration.Audience,
            claims: claims,
            expires: expire,
            signingCredentials: signinCredentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(securityToken), expire);
    }
    public static (string, DateTime) GenerateRefreshToken(TokenConfiguration configuration, IEnumerable<Claim> claims)
    {
        var key = Encoding.UTF8.GetBytes(configuration.Secret);

        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var expire = DateTime.UtcNow.AddMinutes(configuration.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken(
            issuer: configuration.Issuer,
            audience: configuration.Audience,
            claims: claims,
            expires: expire,
            signingCredentials: signinCredentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(securityToken), expire);
    }
}
