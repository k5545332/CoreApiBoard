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
using System.Threading.Tasks;

namespace CoreApiBoard.Services
{
    class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ThemeService(IThemeRepository themeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _themeRepository = themeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public string IndexGetData()
        {
            var IndexThemeDto = new IndexThemeGetDto();

            var Data = _themeRepository.IndexGetAll();

            IndexThemeDto.ThemeDtos = Data;

            return JsonConvert.SerializeObject(IndexThemeDto);
        }


        public string CreateThemeSubmitData(CreateThemeSubmitDto data)
        {
            try
            {
                var Claims = _httpContextAccessor.HttpContext.User.Claims;

                var Data = _mapper.Map<ThemeDto>(data);
                Data.UpdateTime = DateTime.Now;
                Data.Del = false;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

                return _themeRepository.Create(Data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateThemeGetData(int id)
        {
            var Data = _themeRepository.UpdateGet(id);
            if (Data!=null)
            {
                return JsonConvert.SerializeObject(Data);
            }
            return "查無資料";
        }

        public string UpdateThemeSubmitData(UpdateThemeSubmitDto data)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Data = _mapper.Map<ThemeDto>(data);
            Data.UpdateTime = DateTime.Now;
            Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

            return _themeRepository.Update(Data);
        }

        public string DeleteThemeData(int id)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Data = _themeRepository.DeleteGet(id);
            if (Data != null)
            {
                Data.UpdateTime = DateTime.Now;
                Data.Del = true;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());
                return _themeRepository.Delete(Data);
            }
            return "查無資料";
        }

    }
}
