using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApiBoard.Services
{
    class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IThemeRepository _themeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IThemeRepository themeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _themeRepository = themeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public string IndexGetData()
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Userid = 0;
            var AccessLevelid = 0;

            if (Claims.Count() > 0)
            {
                Int32.TryParse(Claims.Where(x => x.Type == "Userid").First().Value.ToString(), out Userid);
                Int32.TryParse(Claims.Where(x => x.Type == ClaimTypes.Role).First().Value.ToString(), out AccessLevelid);
            }
            

            var IndexEventDto = new IndexEventGetDto();
            
            if (Userid==0) //未登入
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll().Where(x => x.Enabled == true);
            }
            else if (AccessLevelid == 1) //管理者
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll();
            }
            else
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll().Where(x => x.Userid == Userid);
            }

            return JsonConvert.SerializeObject(IndexEventDto);
        }
        public string GetData(int id)
        {
            var DetailEventDto = _mapper.Map<DetailEventDto>(_eventRepository.Get(id));
            return JsonConvert.SerializeObject(DetailEventDto);
        }

        public string CreateEventGetData()
        {
            var CreateEventGetDto = new CreateEventGetDto();
            var ThemeForEventDto = _mapper.Map<IEnumerable<ThemeForEventDto>>(_themeRepository.GetAll().Where(x => x.Enabled == true));

            CreateEventGetDto.ThemeDtos = ThemeForEventDto;
            return JsonConvert.SerializeObject(CreateEventGetDto);
        }

        public string CreateEventSubmitData(CreateEventSubmitDto data)
        {
            try
            {
                var Claims = _httpContextAccessor.HttpContext.User.Claims;

                var Data = _mapper.Map<EventDto>(data);
                Data.UpdateTime = DateTime.Now;
                Data.Views = 0;
                Data.Del = false;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

                return _eventRepository.Create(Data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateEventGetData(int id)
        {
            var Data = _eventRepository.UpdateGet(id);
            if (Data!=null)
            {
                var UpdateEventGetDto = new UpdateEventGetDto
                {
                    UpdateEventDto = Data
                };
                var ThemeForEventDto = _mapper.Map<IEnumerable<ThemeForEventDto>>(_themeRepository.GetAll().Where(x => x.Enabled == true));
                UpdateEventGetDto.ThemeDtos = ThemeForEventDto;
                return JsonConvert.SerializeObject(UpdateEventGetDto);
            }
            return "查無資料";
        }

        public string UpdateEventSubmitData(UpdateEventSubmitDto data)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Data = _mapper.Map<EventDto>(data);
            Data.UpdateTime = DateTime.Now;
            Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

            return _eventRepository.Update(Data);
        }

        public string DeleteEventData(int id)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;

            var Data = _eventRepository.DeleteGet(id);
            if (Data != null)
            {
                Data.UpdateTime = DateTime.Now;
                Data.Del = true;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());
                return _eventRepository.Delete(Data);
            }
            return "查無資料";
        }

        public string AddDataView(int id)
        {
            var EventDto = _mapper.Map<EventDto>(_eventRepository.Get(id));
            EventDto.Views += 1;
            var Result = _eventRepository.Update(EventDto);

            return Result;
        }
    }
}
