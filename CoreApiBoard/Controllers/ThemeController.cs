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
    [Route("theme")]
    [Authorize(Roles = "1,2")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        [HttpGet("index")]
        public string GetAll()
        {
            try
            {
                return _themeService.IndexGetData();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("add/submit")]
        public string CreatePost([FromBody] CreateThemeSubmitDto data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Name))
                {
                    return _themeService.CreateThemeSubmitData(data);
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
                return _themeService.UpdateThemeGetData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("update/{id}/submit")]
        public string UpdatePut([FromRoute] int id, [FromBody] UpdateThemeSubmitDto data)
        {
            try
            {
                if (id == data.Themeid)
                {
                    return _themeService.UpdateThemeSubmitData(data);
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
                return _themeService.DeleteThemeData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
