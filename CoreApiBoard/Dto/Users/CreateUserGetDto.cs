using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class CreateUserGetDto
    {
        public IEnumerable<AccessLevelDto> AccessLevelDtos { get; set; }
    }
}
