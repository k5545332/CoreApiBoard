using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.PostgreSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Automapper
{
    public class ThemeProfile : Profile
    {
        public ThemeProfile()
        {
            CreateMap<Theme, ThemeDto>().ReverseMap();
            CreateMap<ThemeDto, ThemeForEventDto>();

            CreateMap<Theme, IndexThemeDto>()
            .ForMember(target => target.UserName, ori => ori.MapFrom(x => x.User.UserName))
            .ForMember(target => target.UpdateTime, ori => ori.MapFrom(x => string.Format("{0:yyyy-MM-dd HH:mm}", x.UpdateTime)));

            CreateMap<CreateThemeSubmitDto, ThemeDto>();
            CreateMap<Theme, UpdateThemeGetDto>();
            CreateMap<UpdateThemeSubmitDto, ThemeDto>();


        }
    }
}
