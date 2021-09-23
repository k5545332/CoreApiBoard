using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Repositorys
{
    class AccessLevelRepository : IAccessLevelRepository
    {
        private readonly BoardContext _boardContext;
        private readonly IMapper _mapper;

        public AccessLevelRepository(BoardContext boardContext, IMapper mapper)
        {
            _boardContext = boardContext;
            _mapper = mapper;
        }
        public IEnumerable<AccessLevelDto> GetAll()
        {

            var data = _boardContext.AccessLevels.Select(x => x);

            var AccessLevelDtos = _mapper.Map<IEnumerable<AccessLevelDto>>(data);

            return AccessLevelDtos;
        }

    }
}