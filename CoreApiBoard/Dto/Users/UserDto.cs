using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Dto.Users
{
    public class UserDto
    {
        public int Userid { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        public int AccessLevelid { get; set; }
        public bool? Enabled { get; set; }
        public string Email { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Visible { get; set; }
        public string Salt { get; set; }
        public bool Del { get; set; }
    }
}
