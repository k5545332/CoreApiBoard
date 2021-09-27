using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.PostgreSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Automapper
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, IndexEventDto>()
            .ForMember(target => target.UserName, ori => ori.MapFrom(x => x.User.UserName))
            .ForMember(target => target.Theme, ori => ori.MapFrom(x => x.Theme.Name))
            .ForMember(target => target.UpdateTime, ori => ori.MapFrom(x => string.Format("{0:yyyy-MM-dd HH:mm}", x.UpdateTime)));

            CreateMap<Event, DetailEventDto>()
            .ForMember(target => target.UserName, ori => ori.MapFrom(x => x.User.UserName))
            .ForMember(target => target.Theme, ori => ori.MapFrom(x => x.Theme.Name))
            .ForMember(target => target.UpdateTime, ori => ori.MapFrom(x => string.Format("{0:yyyy-MM-dd HH:mm}", x.UpdateTime)));
            
            CreateMap<CreateEventSubmitDto, EventDto>();

            CreateMap<EventDto, Event>().ReverseMap();

            CreateMap<Event, UpdateEventDto>();

            CreateMap<UpdateEventSubmitDto, EventDto>();

            CreateMap<Event, DeleteEventDto>();

            CreateMap<DeleteEventDto, EventDto>();

        }
    }
}
