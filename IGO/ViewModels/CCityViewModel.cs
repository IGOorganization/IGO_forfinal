using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CCityViewModel
    {
        private TOrderDetail _od;
        private TCity _c;
        private TProduct _prod;
        private TProductsPhoto _pho;
        private TTicketAndProduct _tp;
        private TTicketType _t;
        private TSupplier _s;
        public CCityViewModel()
        {
            _od = new TOrderDetail();
            _c = new TCity();
            _prod = new TProduct();
            _pho = new TProductsPhoto();
            _tp = new TTicketAndProduct();
            _t = new TTicketType();
            _s = new TSupplier();
        }
        //TSupplier
        public TSupplier tSupplier
        {
            get { return _s; }
            set { _s = value; }
        }
        public string CompanyName
        {
            get { return _s.FCompanyName; }
            set { _s.FCompanyName = value; }
        }


        //TCity
        public TCity tCity
        {
            get { return _c; }
            set { _c = value; }
        }
        public int CityId
        {
            get { return _c.FCityId; }
            set { _c.FCityId = value; }
        }
        public string CityName
        {
            get { return _c.FCityName; }
            set { _c.FCityName = value; }
        }
        public string CityPhotoPath
        {
            get { return _c.FCityPhotoPath; }
            set { _c.FCityPhotoPath = value; }
        }
        //TOrderDetail
        public TOrderDetail tOrderdetail
        {
            get { return _od; }
            set { _od = value; }
        }
        public int orderDetailsId
        {
            get { return _od.FOrderDetailsId; }
            set { _od.FOrderDetailsId = value; }
        }
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
        public string EndTime
        {
            get { return _prod.FEndTime; }
            set { _prod.FEndTime = value; }
        }
        public int? StorageQuantity
        {
            get { return _prod.FQuantity; }
            set { _prod.FQuantity = value; }
        }
        // TProductsPhoto
        public TProductsPhoto tProductsPhoto
        {
            get { return _pho; }
            set { _pho = value; }
        }
        public int ProductPhotoId
        {
            get { return _pho.FProductPhotoId; }
            set { _pho.FProductPhotoId = value; }
        }
        public string PhotoPath
        {
            get { return _pho.FPhotoPath; }
            set { _pho.FPhotoPath = value; }
        }
        public int? PhotoSiteId
        {
            get { return _pho.FPhotoSiteId; }
            set { _pho.FPhotoSiteId = value; }
        }
        //TTicketAndProduct
        public TTicketAndProduct tTicketAndProduct
        {
            get { return _tp; }
            set { _tp = value; }
        }
        public decimal? Price
        {
            get { return _tp.FPrice; }
            set { _tp.FPrice = value; }
        }
        public int? TicketId
        {
            get { return _tp.FTicketId; }
            set { _tp.FTicketId = value; }
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
    }
}
