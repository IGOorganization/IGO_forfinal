using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TTicketAndProduct
    {
        public int FTicketAndProductId { get; set; }
        public int? FTicketId { get; set; }
        public int? FProductId { get; set; }
        public decimal? FPrice { get; set; }

        public virtual TProduct FProduct { get; set; }
        public virtual TTicketType FTicket { get; set; }
    }
}
