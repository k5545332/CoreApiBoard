using System;
using System.Collections.Generic;

#nullable disable

namespace CoreApiBoard.Models
{
    public partial class AccessLevel
    {
        public AccessLevel()
        {
            Users = new HashSet<User>();
        }

        public int AccessLevelid { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
