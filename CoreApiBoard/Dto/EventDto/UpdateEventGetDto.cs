using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class UpdateEventGetDto
    {
        public UpdateEventDto UpdateEventDto { get; set; }
        public IEnumerable<ThemeForEventDto> ThemeDtos { get; set; }
    }
}
