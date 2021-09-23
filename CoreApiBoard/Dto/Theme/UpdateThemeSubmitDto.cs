using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class UpdateThemeSubmitDto
    {
        public int Themeid { get; set; }
        public string Name { get; set; }
        public bool? Enabled { get; set; }
    }
}
