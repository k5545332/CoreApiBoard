using CoreApiBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IRepositorys
{
    public interface IAccessLevelRepository
    {
        public IEnumerable<AccessLevelDto> GetAll();
       
    }
}
