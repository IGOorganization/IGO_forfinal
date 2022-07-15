using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TPayment
    {
        public TPayment()
        {
            TOrders = new HashSet<TOrder>();
        }

        public int FPayTypeId { get; set; }
        public string FPayType { get; set; }

        public virtual ICollection<TOrder> TOrders { get; set; }
    }
}
