using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO
{
    public class CouponViewModel
    {
        private TCoupon _Cou;
        private DemoIgoContext _dbIgo;
        
        public CouponViewModel(DemoIgoContext db)
        {
            _Cou = new TCoupon();
            _dbIgo = db;
        }
        public TCoupon coupon;
        //public TCoupon coupon
        //{
        //    get { return _Cou; }
        //    set { _Cou = value; }
        //}
        public int FCouponId
        {
            get { return coupon.FCouponId; }
            set { coupon.FCouponId = value; }
        }
        [DisplayName("商品名稱")]
        public string FCouponName
        {
            get { return coupon.FCouponName; }
            set { coupon.FCouponName = value; }
        }
        [DisplayName("項目一")]
        public int? FProductId1
        {
            get { return coupon.FProductId1; }
            set { coupon.FProductId1 = value; }
        }
        [DisplayName("項目二")]
        public int? FProductId2
        {
            get { return coupon.FProductId2; }
            set { coupon.FProductId2 = value; }
        }
        [DisplayName("項目三")]
        public int? FProductId3
        {
            get { return coupon.FProductId3; }
            set { coupon.FProductId3 = value; }
        }
        [DisplayName("項目四")]
        public int? FProductId4
        {
            get { return coupon.FProductId4; }
            set { coupon.FProductId4 = value; }
        }
        [DisplayName("項目五")]
        public int? FProductId5
        {
            get { return coupon.FProductId5; }
            set { coupon.FProductId5 = value; }
        }
        [DisplayName("庫存量")]
        public int? FQuantity
        {
            get { return coupon.FQuantity; }
            set { coupon.FQuantity = value; }
        }
        [DisplayName("折數")]
        public string FDiscount
        {
            get { return coupon.FDiscount; }
            set { coupon.FDiscount = value; }
        }
        [DisplayName("期限")]
        public string FDeadTime
        {
            get { return coupon.FDeadTime; }
            set { coupon.FDeadTime = value; }
        }
        public bool? FTimeOut
        {
            get { return coupon.FTimeOut; }
            set { coupon.FTimeOut = value; }
        }
        [DisplayName("封面圖片")]
        public string FCouponImage
        {
            get { return coupon.FCouponImage; }
            set { coupon.FCouponImage = value; }
        }

        //public virtual TProduct FProductId1Navigation { get; set; }
        //public virtual TProduct FProductId2Navigation { get; set; }
        //public virtual TProduct FProductId3Navigation { get; set; }
        //public virtual TProduct FProductId4Navigation { get; set; }
        //public virtual TProduct FProductId5Navigation { get; set; }
        //public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        //public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }


        public List<CProductViewModel> VMproducts
        {
            get { return getVMproducts(); }
        }
        private List<CProductViewModel> getVMproducts()
        {
            List<CProductViewModel> list = new List<CProductViewModel>();
            list.Add(VMproduct1);
            list.Add(VMproduct2);
            if (VMproduct3 != null)
            {
                list.Add(VMproduct3);
                if (VMproduct4 != null)
                {
                    list.Add(VMproduct4);
                    if (VMproduct5 != null)
                        list.Add(VMproduct5);
                }
            }
            return list;
        }
        public CProductViewModel VMproduct1
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FCity).Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId1);
                if (prod != null)
                {
                    CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                    VMprod.product = prod;
                    return VMprod;
                }

                return null;
            }
        }
        public CProductViewModel VMproduct2
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId2);
                if (prod != null)
                {
                    CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                    VMprod.product = prod;
                    return VMprod;
                }
                return null;
            }
        }
        public CProductViewModel VMproduct3
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId3);
                if (prod != null)
                {
                    CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                    VMprod.product = prod;
                    return VMprod;
                }
                return null;
            }
        }
        public CProductViewModel VMproduct4
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId4);
                if (prod != null)
                {
                    CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                    VMprod.product = prod;
                    return VMprod;
                }
                return null;
            }
        }
        public CProductViewModel VMproduct5
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId5);
                if (prod != null)
                {
                    CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                    VMprod.product = prod;
                    return VMprod;
                }
                return null;
            }
        }
        public TimeSpan endtime
        {
            get
            {
                return Convert.ToDateTime(FDeadTime).Date - DateTime.Now.Date;
            }
        }
        public int totalSoldOut
        {
            get
            {
                int num = 0;
                foreach(CSoldOut s in Solded)
                {
                    num += s.SoldedNum;
                }

                return num;
            }
        }

        public List<CSoldOut> Solded
        {
            get
            {
                List<CSoldOut> list = new List<CSoldOut>();
                int day = endtime.Days;
                
                for (int i = 0; i <= day; i++)
                {
                    CSoldOut soldout = new CSoldOut();
                    soldout.Date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                    soldout.day = DateTime.Now.AddDays(i).Day;

                    int a = 0;
                    IEnumerable<TOrderDetail> q = _dbIgo.TOrderDetails.Where(n => n.FCouponId == FCouponId && n.FBookingTime == soldout.Date);
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
