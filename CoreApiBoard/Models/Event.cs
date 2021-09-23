using System;
using System.Collections.Generic;

#nullable disable

namespace CoreApiBoard.Models
{
    public partial class Event
    {
        public Event()
        {
            Photos = new HashSet<Photo>();
        }

        public int Eventid { get; set; }
        public int Userid { get; set; }
        public int Themeid { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public string ContentDes { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int Views { get; set; }
        public bool Del { get; set; }

        public virtual Theme Theme { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
