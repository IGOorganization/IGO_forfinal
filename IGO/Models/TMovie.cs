using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TMovie
    {
        public int MovieId { get; set; }
        public string Cname { get; set; }
        public string Description { get; set; }
        public string Ename { get; set; }
        public string Type { get; set; }
        public int? Time { get; set; }
    }
}
