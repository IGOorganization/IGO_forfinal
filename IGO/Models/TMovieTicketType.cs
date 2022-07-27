using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TMovieTicketType
    {
        public TMovieTicketType()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int FTicketTypeId { get; set; }
        public string FTicketName { get; set; }
        public int? FPrice { get; set; }

        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
