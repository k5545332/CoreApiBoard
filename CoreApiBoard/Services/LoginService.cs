using AutoMapper;
using CoreApiBoard.Dto.Login;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CoreApiBoard.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper jwt;

        public LoginService(IUserRepository userRepository, JwtHelper jwt)
        {
            _userRepository = userRepository;
            this.jwt = jwt;
        }

        public string Login(LoginDto LoginDto)
        {
            var UserData = _userRepository.Get(LoginDto.Account);
            if (UserData == null)
            {
                return "沒有此帳號";
            }
            if (UserData.Enabled == false)
            {
                return "帳號未啟用";
            }

            //var CheckPassword = UsersService.AesEncryptBase64(LoginDto.Password, UserData.Salt);//將前台來的密碼加密後比較
            if (LoginDto.Password == UserData.Password)
            {
                return jwt.GetJwtToken(LoginDto.Account);
            }
            else
            {
                return "密碼錯誤";
            }
        }
       
    }
}
