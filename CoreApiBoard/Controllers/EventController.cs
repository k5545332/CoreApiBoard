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
    [Route("event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("index")]
        public string GetAll()
        {
            try
            {
                return _eventService.IndexGetData();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("detail/{id}")]
        public string Get(int id)
        {
            try
            {
                var Result = _eventService.AddDataView(id);
                if (Result == "ok")
                {
                    return _eventService.GetData(id);
                }
                return "fail";
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
                return _eventService.CreateEventGetData();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Authorize(Roles = "1,2")]
        [HttpPost("add/submit")]
        public string CreatePost([FromBody] CreateEventSubmitDto data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Title))
                {
                    return _eventService.CreateEventSubmitData(data);
                }
                return "fail";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("update/{id}")]
        public string UpdateGet([FromRoute] int id)
        {
            try
            {
                return _eventService.UpdateEventGetData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpPut("update/{id}/submit")]
        public string UpdatePut([FromRoute] int id, [FromBody] UpdateEventSubmitDto data)
        {
            try
            {
                if (id == data.Eventid)
                {
                    return _eventService.UpdateEventSubmitData(data);
                }
                return "fail";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpDelete("delete/{id}")]
        public string Delete([FromRoute] int id)
        {
            try
            {
                return _eventService.DeleteEventData(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
