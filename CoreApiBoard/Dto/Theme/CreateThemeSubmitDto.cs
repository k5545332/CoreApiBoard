using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class CreateThemeSubmitDto
    {
        public string Name { get; set; }
        public bool? Enabled { get; set; }

    }
}
