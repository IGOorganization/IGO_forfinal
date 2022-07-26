using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TShowing
    {
        public TShowing()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int FShowingId { get; set; }
        public string FShowingName { get; set; }

        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
