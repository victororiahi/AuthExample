using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthExample.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthExample.application.Services
{

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, List<string> roles)
        {
            //Create list of claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("PhoneNumber",user.PhoneNumber),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Prepare token expiration
            var expiration = DateTime.Now.AddMinutes(
                Convert.ToInt32(_configuration["Authentication:JwtBearer:AccessExpiration"]
                ));

            //Create the sign in key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtBearer:SecretKey"]));

            //Create the sign in credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
