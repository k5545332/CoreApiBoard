using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Dto.Users;
using CoreApiBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, IndexUserDto>()
            .ForMember(target => target.AccessLevelName, ori => ori.MapFrom(x => x.AccessLevel.Name))
            .ForMember(target => target.UpdateTime, ori => ori.MapFrom(x => string.Format("{0:yyyy-MM-dd HH:mm}", x.UpdateTime)));

            CreateMap<CreateUserSubmitDto, UserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserSubmitDto, UserDto>();
        }
    }
}
