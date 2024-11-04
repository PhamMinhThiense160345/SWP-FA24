using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.API.Config;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Implement
{
    public class AuthService
    {
        //private readonly JwtConfig _jwtConfig;

        //public AuthService(JwtConfig jwtConfig)
        //{
        //    _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig), "JWT configuration is required");
        //    if (string.IsNullOrEmpty(_jwtConfig.Secret) || _jwtConfig.Secret.Length < 32)
        //    {
        //        throw new ArgumentException("JWT Secret must be provided and should be at least 32 characters long.");
        //    }
        //}

        //public string GenerateToken(User user)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim("id", user.UserId.ToString())
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1), 
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
