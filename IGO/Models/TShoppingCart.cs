using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TShoppingCart
    {
        public int FShoppingCartId { get; set; }
        public int FProductId { get; set; }
        public int? FCustomerId { get; set; }
        public int? FTicketId { get; set; }
        public int? FQuantity { get; set; }
        public decimal? FTotalPrice { get; set; }
        public string FSeats { get; set; }
        public string FTempOrder { get; set; }
        public string FBookingTime { get; set; }
        public int? FCouponId { get; set; }
        public int? FSupplierId { get; set; }
        public int? FShowingId { get; set; }
        public int? FMovieId { get; set; }
        public int? FMovieSeatId { get; set; }
        public int? FMovieTicketTypeId { get; set; }

        public virtual TCoupon FCoupon { get; set; }
        public virtual TCustomer FCustomer { get; set; }
        public virtual TMovie FMovie { get; set; }
        public virtual TMovieSeat FMovieSeat { get; set; }
        public virtual TMovieTicketType FMovieTicketType { get; set; }
        public virtual TProduct FProduct { get; set; }
        public virtual TShowing FShowing { get; set; }
        public virtual TSupplier FSupplier { get; set; }
    }
}
