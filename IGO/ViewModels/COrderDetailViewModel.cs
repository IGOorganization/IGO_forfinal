using IGO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{

    public class COrderDetailViewModel
    {

        private TOrderDetail _OrderDetail;
        private DemoIgoContext _dbIgo;

        public COrderDetailViewModel(DemoIgoContext db)
        {
            _OrderDetail = new TOrderDetail();
            _dbIgo = db;

        }
        public TOrderDetail orderDetail { get { return _OrderDetail; } set { _OrderDetail = value; } }
        public int FOrderDetailsId { get { return _OrderDetail.FOrderDetailsId; } set { _OrderDetail.FOrderDetailsId = value; } }
        public int FOrderId { get { return _OrderDetail.FOrderId; } set { _OrderDetail.FOrderId = value; } }
        public int? FProductId { get { return _OrderDetail.FProductId; } set { _OrderDetail.FProductId = value; } }
        [DisplayName("使用期限")]
        public string FBookingTime { get { return _OrderDetail.FBookingTime; } set { _OrderDetail.FBookingTime = value; } }
        public int? FTicketId { get { return _OrderDetail.FTicketId; } set { _OrderDetail.FTicketId = value; } }
        public int? FCouponId { get { return _OrderDetail.FCouponId; } set { _OrderDetail.FCouponId = value; } }
        [DisplayName("張數")]
        public int? FQuantity { get { return _OrderDetail.FQuantity; } set { _OrderDetail.FQuantity = value; } }
        [DisplayName("價格")]
        public decimal? FPrice { get { return _OrderDetail.FPrice; } set { _OrderDetail.FPrice = value; } }
        [DisplayName("訂單編號")]
        public TOrder order { get { return _dbIgo.TOrders.FirstOrDefault(t => t.FOrderId == FOrderId); } }

        [DisplayName("產品名稱")]
        public TProduct product { get { return _dbIgo.TProducts.FirstOrDefault(t => t.FProductId == FProductId); } }
        
        public TTicketType ticket { get { return _dbIgo.TTicketTypes.FirstOrDefault(t => t.FTicketId == FTicketId); } }
        public TCustomer customer { get { return _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == order.FCustomerId); } }
    }
}
