using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TOrderDetail
    {
        public TOrderDetail()
        {
            TSeats = new HashSet<TSeat>();
        }

        public int FOrderDetailsId { get; set; }
        public int FOrderId { get; set; }
        public int? FProductId { get; set; }
        public string FBookingTime { get; set; }
        public int? FTicketId { get; set; }
        public int? FCouponId { get; set; }
        public int? FQuantity { get; set; }
        public decimal? FPrice { get; set; }
        public int? FSupplierId { get; set; }
        public int? FShowingId { get; set; }
        public int? FMovieId { get; set; }
        public int? FMovieSeatId { get; set; }
        public int? FMovieTicketType { get; set; }

        public virtual TCoupon FCoupon { get; set; }
        public virtual TMovie FMovie { get; set; }
        public virtual TMovieSeat FMovieSeat { get; set; }
        public virtual TMovieTicketType FMovieTicketTypeNavigation { get; set; }
        public virtual TOrder FOrder { get; set; }
        public virtual TProduct FProduct { get; set; }
        public virtual TShowing FShowing { get; set; }
        public virtual TSupplier FSupplier { get; set; }
        public virtual TTicketType FTicket { get; set; }
        public virtual ICollection<TSeat> TSeats { get; set; }
    }
}
