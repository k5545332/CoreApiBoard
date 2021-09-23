using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class IndexThemeDto
    {
        public int Themeid { get; set; }
        public string Name { get; set; }
        public bool? Enabled { get; set; }
        public string UpdateTime { get; set; }
        public string UserName { get; set; }

    }
}
