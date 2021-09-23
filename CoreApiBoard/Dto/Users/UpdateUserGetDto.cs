using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class UpdateUserGetDto
    {
        public UpdateUserDto UpdateUserDto { get; set; }
        public IEnumerable<AccessLevelDto> AccessLevelDtos { get; set; }
    }
}
