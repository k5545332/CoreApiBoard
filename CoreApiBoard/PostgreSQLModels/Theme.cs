using System;
using System.Collections.Generic;

#nullable disable

namespace CoreApiBoard.PostgreSQLModels
{
    public partial class Theme
    {
        public Theme()
        {
            Events = new HashSet<Event>();
        }

        public int Themeid { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Userid { get; set; }
        public bool Del { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
