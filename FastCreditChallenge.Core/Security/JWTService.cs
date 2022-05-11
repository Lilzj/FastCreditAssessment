using FastCreditChallenge.Data.Settings;
using FastCreditChallenge.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Core.Security
{
    public static class JWTService
    {
        public static string GenerateToken(User loggedinUser, IList<string> roles, IOptions<JWTData> options)
        {
            var JWTData = options.Value;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loggedinUser.UserName),
                new Claim(ClaimTypes.Email, loggedinUser.Email),
                new Claim(ClaimTypes.NameIdentifier, loggedinUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Create security token descriptor
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTData.SecretKey));

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                Audience = JWTData.Audience,
                Issuer = JWTData.Issuer,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
