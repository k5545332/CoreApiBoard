using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.PostgreSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Automapper
{
    public class AccessLevelProfile : Profile
    {
        public AccessLevelProfile()
        {
            CreateMap<AccessLevel, AccessLevelDto>();
        }
    }
}
