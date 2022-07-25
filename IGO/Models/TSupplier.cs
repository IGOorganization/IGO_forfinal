using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TSupplier
    {
        public TSupplier()
        {
            TCustomers = new HashSet<TCustomer>();
            TOrderDetails = new HashSet<TOrderDetail>();
            TProducts = new HashSet<TProduct>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int FSupplierId { get; set; }
        public string FCompanyName { get; set; }
        public string FPhone { get; set; }
        public int? FCityId { get; set; }
        public string FAddress { get; set; }
        public int? FSubCategoryId { get; set; }
        public string FImage { get; set; }

        public virtual TCity FCity { get; set; }
        public virtual TSubCategory FSubCategory { get; set; }
        public virtual ICollection<TCustomer> TCustomers { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
