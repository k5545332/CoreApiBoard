using System;
using System.Collections.Generic;

#nullable disable

namespace CoreApiBoard.Models
{
    public partial class Photo
    {
        public int Photoid { get; set; }
        public int? Eventid { get; set; }
        public string PicUrl { get; set; }
        public string PicDetail { get; set; }
        public int Sort { get; set; }
        public int Size { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Del { get; set; }

        public virtual Event Event { get; set; }
    }
}
