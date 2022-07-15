using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TStatus
    {
        public TStatus()
        {
            TOrders = new HashSet<TOrder>();
        }

        public int FStatusId { get; set; }
        public string FStatusName { get; set; }

        public virtual ICollection<TOrder> TOrders { get; set; }
    }
}
