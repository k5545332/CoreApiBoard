using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto
{
    public class IndexUserDto
    {
        public int Userid { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        public string AccessLevelName { get; set; }
        public bool? Enabled { get; set; }
        public string Email { get; set; }
        public string UpdateTime { get; set; }

    }
}
