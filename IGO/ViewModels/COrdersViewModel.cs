using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class COrdersViewModel
    {
        private TOrder _o;
        private TOrderDetail _od;
        private TStatus _s;
        private TPayment _pay;
        private TProduct _prod;
        private TTicketType _t;

        public COrdersViewModel()
        {

            _o = new TOrder();
            _od = new TOrderDetail();
            _s = new TStatus();
            _pay = new TPayment();
            _prod = new TProduct();
            _t = new TTicketType();

        }
        //TTicketType
        public TTicketType tTicketType
        {
            get { return _t; }
            set { _t = value; }
        }
        public string TicketName
        {
            get { return _t.FTicketName; }
            set { _t.FTicketName = value; }
        }

        public TPayment tPayment
        {
            get { return _pay; }
            set { _pay = value; }
        }
        public string PayType
        {
            get { return _pay.FPayType; }
            set { _pay.FPayType = value; }
        }
        public string StatusName
        {
            get { return _s.FStatusName; }
            set { _s.FStatusName = value; }
        }
        public TOrder tOrder
        {
            get { return _o; }
            set { _o = value; }
        }
        public TOrderDetail tOrderDetail
        {
            get { return _od; }
            set { _od = value; }
        }
        public int OrderDetailsId
        {
            get { return _od.FOrderDetailsId; }
            set { _od.FOrderDetailsId = value; }
        }

        public int? Quantity
        {
            get { return _od.FQuantity; }
            set { _od.FQuantity = value; }
        }
        public decimal? Price
        {
            get { return _od.FPrice; }
            set { _od.FPrice = value; }
        }
        // TProducts
        public TProduct tProduct
        {
            get { return _prod; }
            set { _prod = value; }
        }
        public int ProductId
        {
            get { return _prod.FProductId; }
            set { _prod.FProductId = value; }
        }
        public string ProductName
        {
            get { return _prod.FProductName; }
            set { _prod.FProductName = value; }
        }

        public int OrderId
        {
            get { return _o.FOrderId; }
            set { _o.FOrderId = value; }
        }
        public string OrderDate
        {
            get { return _o.FOrderDate; }
            set { _o.FOrderDate = value; }
        }
        public string OrderNum
        {
            get { return _o.FOrderNum; }
            set { _o.FOrderNum = value; }
        }

        public string ShippedDate
        {
            get { return _o.FShippedDate; }
            set { _o.FShippedDate = value; }
        }

        public int? StatusId
        {
            get { return _o.FStatusId; }
            set { _o.FStatusId = value; }
        }
        public decimal? TotalPrice
        {
            get { return _o.FTotalPrice; }
            set { _o.FTotalPrice = value; }
        }
    }
}
