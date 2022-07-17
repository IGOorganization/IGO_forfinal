using IGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CShoppingCartViewModel
    {
        private TShoppingCart _ShoppingCart;
        private DemoIgoContext _dbIgo;

        public CShoppingCartViewModel(DemoIgoContext db) {
            _ShoppingCart = new TShoppingCart();
            _dbIgo = db;

        }
        public TShoppingCart shoppingCart { get { return _ShoppingCart; }  set { _ShoppingCart = value; } }
        public int FShoppingCartId { get {return _ShoppingCart.FShoppingCartId; } set { _ShoppingCart.FShoppingCartId = value; } }
        public int FProductId { get { return _ShoppingCart.FProductId; } set { _ShoppingCart.FProductId = value; } }
        public int? FCustomerId { get { return _ShoppingCart.FCustomerId; } set { _ShoppingCart.FCustomerId = value; } }
        public int? FTicketId { get { return _ShoppingCart.FTicketId; } set { _ShoppingCart.FTicketId = value; } }
        [DisplayName("數量")]
        public int? FQuantity { get { return _ShoppingCart.FQuantity; } set { _ShoppingCart.FQuantity = value; } }

        [DisplayName("價錢")]
        public decimal? FTotalPrice { get { return _ShoppingCart.FTotalPrice; } set { _ShoppingCart.FTotalPrice = value; } }
        public string FSeats { get { return _ShoppingCart.FSeats; } set { _ShoppingCart.FSeats = value; } }
        public string FTempOrder { get { return _ShoppingCart.FTempOrder; } set { _ShoppingCart.FTempOrder = value; } }

        [DisplayName("使用日期")]
        public string FBookingTime { get { return _ShoppingCart.FBookingTime; } set { _ShoppingCart.FBookingTime = value; } }
        public int? FCouponId { get { return _ShoppingCart.FCouponId; } set { _ShoppingCart.FCouponId = value; } }

      
        public virtual TCoupon FCoupon { get; set; }
        public virtual TCustomer FCustomer { get; set; }
        public virtual TProduct FProduct { get; set; }

        
        public TProduct product
        {
            get {
                TProduct prod = _dbIgo.TProducts.Where(p => p.FProductId == FProductId).FirstOrDefault();
                if (prod != null)
                    return prod;
                return null;
            }
        }
    }
}
