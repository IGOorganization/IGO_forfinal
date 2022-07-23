using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CHomeViewModel
    {
        private TCustomer _cust; //注入
        private TProduct _prod;
        private TOrder _o;
        private TOrderDetail _od;
        private TCity _c;
        private TProductsPhoto _pho;
        private TFeedbackManagement _fb;
        private TTicketAndProduct _tp;
        private TTicketType _t;
        private TCategory _cg;
        private TSubCategory _sb;
        public CHomeViewModel()
        {
            _cust = new TCustomer();
            _prod = new TProduct();
            _o = new TOrder();
            _od = new TOrderDetail();
            _c = new TCity();
            _pho = new TProductsPhoto();
            _fb = new TFeedbackManagement();
            _tp = new TTicketAndProduct();
            _t = new TTicketType();
            _cg = new TCategory();
            _sb = new TSubCategory();
        }
        //TSubCategory
        public TSubCategory tSubCategory
        {
            get { return _sb; }
            set { _sb = value; }
        }
        public int SubCategoryId
        {
            get { return _sb.FSubCategoryId; }
            set { _sb.FSubCategoryId = value; }
        }
        public string SubCategoryName
        {
            get { return _sb.FSubCategoryName; }
            set { _sb.FSubCategoryName = value; }
        }
        public string SubCategoryPath
        {
            get { return _sb.FSubCategoryPath; }
            set { _sb.FSubCategoryPath = value; }
        }
        public List<string> SubCategoryNameList
        {
            get;
            set;
        }
        public List<SubcatergoryClass> SubCategoryList
        {
            get;
            set;
        }

        //TCategory
        public TCategory tCategory
        {
            get { return _cg; }
            set { _cg = value; }
        }
        public int CategoryId
        {
            get { return _cg.FCategoryId; }
            set { _cg.FCategoryId = value; }
        }
        public string CategoryName
        {
            get { return _cg.FCategoryName; }
            set { _cg.FCategoryName = value; }
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
        //TFeedbackManagement
        public TFeedbackManagement tFeedbackManagement
        {
            get { return _fb; }
            set { _fb = value; }
        }
        public int FeedbackId
        {
            get { return _fb.FFeedbackId; }
            set { _fb.FFeedbackId = value; }
        }
        public int? Ranking
        {
            get { return _fb.FRanking; }
            set { _fb.FRanking = value; }
        }
        public string FeedbackContent
        {
            get { return _fb.FFeedbackContent; }
            set { _fb.FFeedbackContent = value; }
        }
        public int RankingCount { get; set; }
        //TCustomer
        public TCustomer tCustomer
        {
            get { return _cust; }
            set { _cust = value; }
        }
        public int CustomerId
        {
            get { return _cust.FCustomerId; }
            set { _cust.FCustomerId = value; }
        }
        public string LastName
        {
            get { return _cust.FLastName; }
            set { _cust.FLastName = value; }
        }
        public int customerCount { get; set; }
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
        //TOrder
        public TOrder order
        {
            get { return _o; }
            set { _o = value; }
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
        public int? OrderQuantity
        {
            get { return _od.FQuantity; }
            set { _od.FQuantity = value; }
        }
    }
}
