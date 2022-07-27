using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TMovieSeat
    {
        public TMovieSeat()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int FSeatId { get; set; }
        public string FSeatRow { get; set; }
        public int? FSeatColumn { get; set; }

        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
