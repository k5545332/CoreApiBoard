using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class ThemeDto
    {
        public int Themeid { get; set; }
        public string Name { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Userid { get; set; }
        public bool Del { get; set; }

    }
}
