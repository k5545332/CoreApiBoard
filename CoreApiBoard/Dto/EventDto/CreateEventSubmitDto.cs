using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class CreateEventSubmitDto
    {
        public int Themeid { get; set; }
        public string Title { get; set; }
        public bool Enabled { get; set; }
        public string ContentDes { get; set; }

    }
}
