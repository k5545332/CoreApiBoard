using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class IndexEventGetDto
    {
        public IEnumerable<IndexEventDto> EventDtos { get; set; }

    }
}
