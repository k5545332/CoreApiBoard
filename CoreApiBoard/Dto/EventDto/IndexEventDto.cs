using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class IndexEventDto
    {
        public int Eventid { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public string ContentDes { get; set; }
        public string UpdateTime { get; set; }
        public int Views { get; set; }
    }
}
