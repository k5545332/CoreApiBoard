using System;
using System.Collections.Generic;

#nullable disable

namespace CoreApiBoard.PostgreSQLModels
{
    public partial class User
    {
        public User()
        {
            Events = new HashSet<Event>();
            Themes = new HashSet<Theme>();
        }

        public int Userid { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        public int AccessLevelid { get; set; }
        public bool Enabled { get; set; }
        public string Email { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Visible { get; set; }
        public string Salt { get; set; }
        public bool Del { get; set; }

        public virtual AccessLevel AccessLevel { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
