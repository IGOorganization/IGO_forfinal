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

        public CShoppingCartViewModel(DemoIgoContext db)
        {
            _ShoppingCart = new TShoppingCart();
            _dbIgo = db;

        }
        public TShoppingCart shoppingCart { get { return _ShoppingCart; } set { _ShoppingCart = value; } }
        public int FShoppingCartId { get { return _ShoppingCart.FShoppingCartId; } set { _ShoppingCart.FShoppingCartId = value; } }
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

        public int? FMovieId { get { return _ShoppingCart.FMovieId; } set { _ShoppingCart.FMovieId = value; } }
        public int? FMovieTicketTypeId { get { return _ShoppingCart.FMovieTicketTypeId; } set { _ShoppingCart.FMovieTicketTypeId = value; } }

        public TProduct product
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Where(p => p.FProductId == FProductId).FirstOrDefault();
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public TCustomer customer
        {
            get
            {
                TCustomer cust = _dbIgo.TCustomers.Where(p => p.FCustomerId == FCustomerId).FirstOrDefault();
                if (cust != null)
                    return cust;
                return null;
            }
        }

        public TCity city
        {
            get
            {
                TCity city = _dbIgo.TCities.Where(p => p.FCityId == customer.FCityId).FirstOrDefault();
                if (city != null)
                    return city;
                return null;
            }
        }
        public TTicketType ticket
        {
            get
            {
                TTicketType ticket = _dbIgo.TTicketTypes.Where(p => p.FTicketId == FTicketId).FirstOrDefault();
                if (ticket != null)
                    return ticket;
                return null;
            }
        }
        public List<TVoucher> voucher
        {
            get
            {
                List<TVoucher> voucher = _dbIgo.TVouchers.Where(c => c.FCustomerId == FCustomerId).ToList();
                return voucher;
            }

        }

        public TProductsPhoto photo
        {
            get
            {
                TProductsPhoto photo = _dbIgo.TProductsPhotos.FirstOrDefault(c => FMovieId > 0 ? c.FMovieId == FMovieId : c.FProductId == FProductId);
                return photo;
            }

        }
        public string introduction
        {
            get
            {
                if (FMovieId > 0)
                {
                    string introduction = _dbIgo.TMovies.FirstOrDefault(c => c.MovieId == FMovieId).Description;
                    return introduction;
                }
                else
                {
                    string introduction = _dbIgo.TProducts.FirstOrDefault(c => c.FProductId == FProductId).FIntroduction;
                    return introduction;
                }
            }
        }
        public string ticketName
        {
            get
            {
                if (FMovieTicketTypeId > 0)
                {
                    string ticketName = _dbIgo.TMovieTicketTypes.FirstOrDefault(c => c.FTicketTypeId == FMovieTicketTypeId).FTicketName;
                    return ticketName;
                }
                else
                {
                    string ticketName = _dbIgo.TTicketTypes.FirstOrDefault(c => c.FTicketId == FTicketId).FTicketName;
                    return ticketName;
                }
            }
        }


        //顯示所有票券
        public string TotalProductName
        {

            get
            {


                if (FMovieId > 0)
                {
                    string TotalProductName = (_dbIgo.TMovies.FirstOrDefault(c => c.MovieId == FMovieId)).Cname;
                    return TotalProductName;
                }
                else if (FCouponId > 0)
                {
                    string TotalProductName = (_dbIgo.TCoupons.FirstOrDefault(c => c.FCouponId == FCouponId)).FCouponName;
                    return TotalProductName;

                }
                else
                {
                    string TotalProductName = (_dbIgo.TProducts.FirstOrDefault(c => c.FProductId == FProductId)).FProductName;
                    return TotalProductName;

                }




            }


        }
       public List<productInfo> productInfos;

    }
    
}
