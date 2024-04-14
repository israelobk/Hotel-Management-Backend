using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace yard.api.Utility
{
    public static class JwtImplementation
    {
        public static string GetJWTToken(IConfiguration configuration, List<Claim> claims)
        {
            // Generate a 128-bit (16-byte) key for HS256
            var key = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            // Convert the byte array to a string
            var base64Key = Convert.ToBase64String(key);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(base64Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(secToken);

            return token;
        }
    }
}


