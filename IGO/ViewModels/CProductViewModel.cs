using IGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CProductViewModel
    {
        private DemoIgoContext _dbIgo;
        // private TProduct _prod = new TProduct();
        public CProductViewModel(DemoIgoContext db)
        {
            _dbIgo = db;
        }

        public TProduct product;
        
        public int FProductId
        {
            get
            { return product.FProductId; }
            set
            { product.FProductId = value; }
        }
        [DisplayName("商品名稱")]
        public string FProductName
        {
            get
            { return product.FProductName; }
            set
            { product.FProductName = value; }
        }
        [DisplayName("縣市")]
        public int? FCityId
        {
            get
            { return product.FCityId; }
            set
            { product.FCityId = value; }
        }
        [DisplayName("地址")]
        public string FAddress
        {
            get
            { return product.FAddress; }
            set
            { product.FAddress = value; }
        }
        [DisplayName("供應商")]
        public int? FSupplierId
        {
            get
            { return product.FSupplierId; }
            set
            { product.FSupplierId = value; }
        }
        [DisplayName("庫存量")]
        public int? FQuantity
        {
            get
            { return product.FQuantity; }
            set
            { product.FQuantity = value; }
        }
        [DisplayName("上架時間")]
        public string FStartTime
        {
            get
            { return product.FStartTime; }
            set
            { product.FStartTime = value; }
        }
        [DisplayName("下架時間")]
        public string FEndTime
        {
            get
            { return product.FEndTime; }
            set
            { product.FEndTime = value; }
        }
        [DisplayName("供應商")]
        public int? FSubCategoryId
        {
            get
            { return product.FSubCategoryId; }
            set
            { product.FSubCategoryId = value; }
        }
        [DisplayName("商品介紹")]
        public string FIntroduction
        {
            get
            { return product.FIntroduction; }
            set
            { product.FIntroduction = value; }
        }
        [DisplayName("是否打折")]
        public bool? FDiscountOrNot
        {
            get
            { return product.FDiscountOrNot; }
            set
            { product.FDiscountOrNot = value; }
        }
        public string fPhotoPath { get; set; }
        public string getPhotoPath
        {
            get
            { return getPath(); }
        }
        private string getPath()
        {
            TProductsPhoto q = _dbIgo.TProductsPhotos.FirstOrDefault(n => n.FPhotoSiteId == 3 & n.FProductId == FProductId);
            if (q != null)
            {
                string s = q.FPhotoPath;
                return s;
            }
            return null;
        }
        //public decimal? price
        //{
        //    get
        //    {
        //        IEnumerable<TTicketType> t = _dbIgo.TTicketTypes.Where(n => n.FSubCategoryId == FSubCategoryId);
        //        return _dbIgo.TTicketAndProducts.Where(n => n.FTicketId == t.First().FTicketId && n.FProductId==FProductId).Select(n => n.FPrice).FirstOrDefault();
        //    }
        //}
        public string CityName
        {
            get
            { return _dbIgo.TProducts.Include(n => n.FCity).FirstOrDefault(n => n.FProductId == FProductId).FCity.FCityName; }
        }
        public string SupplierName
        {
            get
            {
                string s = (_dbIgo.TProducts.Include(n => n.FSupplier).FirstOrDefault(n => n.FProductId == FProductId)).FSupplier.FCompanyName;
                return s;
            }
        }
        public List<CTicketPrice> tickets
        {
            get
            {
                List<CTicketPrice> list = new List<CTicketPrice>();
                IEnumerable<TTicketAndProduct> q = _dbIgo.TTicketAndProducts.Include(n => n.FTicket).Where(n => n.FProductId == FProductId);
                foreach (TTicketAndProduct t in q)
                {
                    CTicketPrice tp = new CTicketPrice();
                    tp.ticketid = t.FTicketId;
                    tp.ticket = t.FTicket.FTicketName;
                    tp.price = (decimal)t.FPrice;
                    list.Add(tp);
                }
                return list;
            }
        }

        public TimeSpan endtime
        {
            get
            {
                return Convert.ToDateTime(FEndTime).Date - DateTime.Now.Date;
            }
        }
        public int ticketid { get; set; }
        public List<CSoldOut> Solded
        {
            get
            {
                List<CSoldOut> list = new List<CSoldOut>();

                for (int i = 0; i <= 365; i++)
                {
                    CSoldOut soldout = new CSoldOut();
                    soldout.Date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                    soldout.day = DateTime.Now.AddDays(i).Day;

                    int a = 0;
                    IEnumerable<TOrderDetail> q = _dbIgo.TOrderDetails.Where(n => n.FProductId == FProductId && n.FBookingTime == soldout.Date&&n.FTicketId==ticketid);
                    foreach (TOrderDetail od in q)
                    {
                        a += (int)od.FQuantity;
                    }
                    soldout.SoldedNum = (int)FQuantity - a;

                    list.Add(soldout);
                }
                return list;
            }

        }
    }
}
