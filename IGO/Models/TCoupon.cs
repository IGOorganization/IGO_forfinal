using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCoupon
    {
        public TCoupon()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int FCouponId { get; set; }
        public string FCouponName { get; set; }
        public int? FProductId1 { get; set; }
        public int? FProductId2 { get; set; }
        public int? FProductId3 { get; set; }
        public int? FProductId4 { get; set; }
        public int? FProductId5 { get; set; }
        public string FDiscount { get; set; }
        public string FDeadTime { get; set; }
        public bool? FTimeOut { get; set; }

        public virtual TProduct FProductId1Navigation { get; set; }
        public virtual TProduct FProductId2Navigation { get; set; }
        public virtual TProduct FProductId3Navigation { get; set; }
        public virtual TProduct FProductId4Navigation { get; set; }
        public virtual TProduct FProductId5Navigation { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
