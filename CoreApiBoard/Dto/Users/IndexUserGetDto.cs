using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class IndexUserGetDto
    {
        public IEnumerable<IndexUserDto> UserDtos { get; set; }

    }
}
