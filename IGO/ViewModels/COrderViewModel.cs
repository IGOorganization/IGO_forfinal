using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class COrderViewModel
    {
        private TOrder _order;
        private DemoIgoContext _dbIgo;

        public COrderViewModel(DemoIgoContext db)
        {
            _order = new TOrder();
            _dbIgo = db;

        }

        public TOrder order { get { return _order; } set { _order = value; } }
        public int FOrderId { get { return _order.FOrderId; } set { _order.FOrderId = value; } }
        public int FCustomerId { get { return _order.FCustomerId; } set { _order.FCustomerId = value; } }
        public string FOrderNum { get { return _order.FOrderNum; } set { _order.FOrderNum = value; } }
        public string FOrderDate { get { return _order.FOrderDate; } set { _order.FOrderDate = value; } }
        public int? FPayTypeId { get { return _order.FPayTypeId; } set { _order.FPayTypeId = value; } }
        public int? FShipperId { get { return _order.FShipperId; } set { _order.FShipperId = value; } }
        public string FShippedDate { get { return _order.FShippedDate; } set { _order.FShippedDate = value; } }
        public int? FStatusId { get { return _order.FStatusId; } set { _order.FStatusId = value; } }
        public decimal? FTotalPrice { get { return _order.FTotalPrice; } set { _order.FTotalPrice = value; } }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TPayment FPayType { get; set; }
        public virtual TShipper FShipper { get; set; }
        public virtual TStatus FStatus { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }

       public List<TOrderDetail> orderDetail { get { return _dbIgo.TOrderDetails.Where(t => t.FOrderId == _order.FOrderId).ToList(); } }

        public TCustomer customer { get { return _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == order.FCustomerId); } }
    }
}
