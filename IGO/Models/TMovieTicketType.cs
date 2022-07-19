using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TMovieTicketType
    {
        public int FTicketTypeId { get; set; }
        public string FTicketName { get; set; }
        public int? FPrice { get; set; }
    }
}
