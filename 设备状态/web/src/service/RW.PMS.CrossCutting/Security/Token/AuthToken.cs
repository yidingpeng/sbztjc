using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.CrossCutting.Security.Token
{
    public class AuthToken : IAuthToken
    {
        private readonly IConfiguration _configuration;

        public AuthToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Create(params Claim[] claims)
        {
            var signingCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SigningKey"])),
                    SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecurityKey"])),
                JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.CreateJwtSecurityToken(
                _configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                new ClaimsIdentity(claims),
                DateTime.Now,
                DateTime.Now.AddMinutes(_configuration["JwtConfig:Expires"].To<int>()),
                null,
                signingCredentials,
                encryptingCredentials
            );
            return jwtHandler.WriteToken(jwtSecurityToken);
        }
    }
}