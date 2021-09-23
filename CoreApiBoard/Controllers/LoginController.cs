using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiBoard.Dto.Login;
using CoreApiBoard.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiBoard.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("submit")]
        public string Index(LoginDto LoginDto)
        {
            try
            {
                if (!(string.IsNullOrEmpty(LoginDto.Account) || string.IsNullOrEmpty(LoginDto.Password)))
                {
                    var Result = _loginService.Login(LoginDto);
                    return Result;
                }
                return "fail";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
