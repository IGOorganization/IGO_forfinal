using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TOrder
    {
        public TOrder()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
        }

        public int FOrderId { get; set; }
        public int FCustomerId { get; set; }
        public string FOrderDate { get; set; }
        public int? FPayTypeId { get; set; }
        public int? FShipperId { get; set; }
        public string FShippedDate { get; set; }
        public int? FStatusId { get; set; }
        public decimal? FTotalPrice { get; set; }
        public string FOrderNum { get; set; }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TPayment FPayType { get; set; }
        public virtual TShipper FShipper { get; set; }
        public virtual TStatus FStatus { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
    }
}
