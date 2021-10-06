using AutoMapper;
using CoreApiBoard.Interfaces.IRepositorys;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiBoard.JWT
{
    public class JwtHelper
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration Configuration { get; }
        public JwtHelper(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            Configuration = configuration;
        }

        public string GetJwtToken(string Account)
        {
            var UserInfo = _userRepository.Get(Account);

            string Secret = Environment.GetEnvironmentVariable("SignKey");
            //string Secret = Configuration.GetValue<string>("JwtSettings:SignKey");

            var userClaims = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, UserInfo.AccessLevelid.ToString()),
                new Claim("Userid", UserInfo.Userid.ToString()),
                new Claim("Name", UserInfo.UserName)
            });
            var userClaimsIdentity = new ClaimsIdentity(userClaims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                //Issuer = Configuration.GetValue<string>("JwtSettings:Issuer"),
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(90),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(securityToken);

            return Token;
        }
    }
}
