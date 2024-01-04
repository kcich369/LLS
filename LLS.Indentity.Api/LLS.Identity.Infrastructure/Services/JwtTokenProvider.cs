using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LLS.Identity.Domain.Configurations;
using LLS.Identity.Domain.Dtos;
using LLS.Identity.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LLS.Identity.Infrastructure.Services;

public class JwtTokenProvider(JwtConfiguration config) : IJwtTokenProvider
{
    private readonly JwtConfiguration _config = config;

    public string GenerateToken(UserData userData)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,userData.Id),
            new Claim(ClaimTypes.Email,userData.Email),
            new Claim(ClaimTypes.MobilePhone,userData.PhoneNumber),
            new Claim(ClaimTypes.Role,userData.Role)
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
        var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var securityToken =
            new JwtSecurityToken(claims: claims, 
                expires: DateTime.Now.AddMinutes(config.ExpiresInMinutes),
                issuer: config.Issuer,
                audience: config.Audience,
                signingCredentials: signingCred);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}