using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class DeleteEventDto
    {
        public int Eventid { get; set; }
        public int Userid { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Del { get; set; }
    }
}
