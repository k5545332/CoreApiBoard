using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiBoard.Dto;
using CoreApiBoard.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiBoard.Controllers
{
    [Route("user")]
    [Authorize(Roles = "1,2")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("index")]
        public string GetAll()
        {
            try
            {
                return _userService.IndexGetData();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Authorize(Roles = "1,2")]
        [HttpGet("add")]
        public string CreateGet()
        {
            try
            {
                return _userService.CreateUserGetData();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("add/submit")]
        public string CreatePost([FromBody] CreateUserSubmitDto data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Account))
                {
                    return _userService.CreateUserSubmitData(data);
                }
                return "fail";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("update/{id}")]
        public string UpdateGet([FromRoute] int id)
        {
            try
            {
                return _userService.UpdateUserGetData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("update/{id}/submit")]
        public string UpdatePut([FromRoute] int id, [FromBody] UpdateUserSubmitDto data)
        {
            try
            {
                if (id == data.Userid)
                {
                    return _userService.UpdateUserSubmitData(data);
                }
                return "fail";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("delete/{id}")]
        public string Delete([FromRoute] int id)
        {
            try
            {
                return _userService.DeleteUserData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
