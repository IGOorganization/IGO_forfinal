using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TShipper
    {
        public TShipper()
        {
            TOrders = new HashSet<TOrder>();
        }

        public int FShipperId { get; set; }
        public string FShipperName { get; set; }
        public string FPhone { get; set; }

        public virtual ICollection<TOrder> TOrders { get; set; }
    }
}
