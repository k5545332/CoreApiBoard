using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class CreateEventGetDto
    {
        public IEnumerable<ThemeForEventDto> ThemeDtos { get; set; }
    }
}
