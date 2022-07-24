using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TProduct
    {
        public TProduct()
        {
            TCollections = new HashSet<TCollection>();
            TCouponFProductId1Navigations = new HashSet<TCoupon>();
            TCouponFProductId2Navigations = new HashSet<TCoupon>();
            TCouponFProductId3Navigations = new HashSet<TCoupon>();
            TCouponFProductId4Navigations = new HashSet<TCoupon>();
            TCouponFProductId5Navigations = new HashSet<TCoupon>();
            TFeedbackManagements = new HashSet<TFeedbackManagement>();
            TOrderDetails = new HashSet<TOrderDetail>();
            TProductsPhotos = new HashSet<TProductsPhoto>();
            TSeats = new HashSet<TSeat>();
            TShoppingCarts = new HashSet<TShoppingCart>();
            TTicketAndProducts = new HashSet<TTicketAndProduct>();
        }

        public int FProductId { get; set; }
        public string FProductName { get; set; }
        public int? FCityId { get; set; }
        public string FAddress { get; set; }
        public int? FSupplierId { get; set; }
        public int? FQuantity { get; set; }
        public string FStartTime { get; set; }
        public string FEndTime { get; set; }
        public int? FSubCategoryId { get; set; }
        public string FIntroduction { get; set; }
        public bool? FDiscountOrNot { get; set; }
        public int? FViewRecord { get; set; }

        public virtual TCity FCity { get; set; }
        public virtual TSubCategory FSubCategory { get; set; }
        public virtual TSupplier FSupplier { get; set; }
        public virtual ICollection<TCollection> TCollections { get; set; }
        public virtual ICollection<TCoupon> TCouponFProductId1Navigations { get; set; }
        public virtual ICollection<TCoupon> TCouponFProductId2Navigations { get; set; }
        public virtual ICollection<TCoupon> TCouponFProductId3Navigations { get; set; }
        public virtual ICollection<TCoupon> TCouponFProductId4Navigations { get; set; }
        public virtual ICollection<TCoupon> TCouponFProductId5Navigations { get; set; }
        public virtual ICollection<TFeedbackManagement> TFeedbackManagements { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TProductsPhoto> TProductsPhotos { get; set; }
        public virtual ICollection<TSeat> TSeats { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
        public virtual ICollection<TTicketAndProduct> TTicketAndProducts { get; set; }
    }
}
