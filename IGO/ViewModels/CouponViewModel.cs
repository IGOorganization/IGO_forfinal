using IGO.Models;
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
        public TCoupon coupon
        {
            get { return _Cou; }
            set { _Cou = value; }
        }
        public int FCouponId
        {
            get { return _Cou.FCouponId; }
            set { _Cou.FCouponId = value; }
        }
        [DisplayName("商品名稱")]
        public string FCouponName
        {
            get { return _Cou.FCouponName; }
            set { _Cou.FCouponName = value; }
        }
        [DisplayName("項目一")]
        public int? FProductId1
        {
            get { return _Cou.FProductId1; }
            set { _Cou.FProductId1 = value; }
        }
        [DisplayName("項目二")]
        public int? FProductId2
        {
            get { return _Cou.FProductId2; }
            set { _Cou.FProductId2 = value; }
        }
        [DisplayName("項目三")]
        public int? FProductId3
        {
            get { return _Cou.FProductId3; }
            set { _Cou.FProductId3 = value; }
        }
        [DisplayName("項目四")]
        public int? FProductId4
        {
            get { return _Cou.FProductId4; }
            set { _Cou.FProductId4 = value; }
        }
        [DisplayName("項目五")]
        public int? FProductId5
        {
            get { return _Cou.FProductId5; }
            set { _Cou.FProductId5 = value; }
        }
        [DisplayName("折數")]
        public string FDiscount
        {
            get { return _Cou.FDiscount; }
            set { _Cou.FDiscount = value; }
        }
        [DisplayName("期限")]
        public string FDeadTime
        {
            get { return _Cou.FDeadTime; }
            set { _Cou.FDeadTime = value; }
        }
        public bool? FTimeOut
        {
            get { return _Cou.FTimeOut; }
            set { _Cou.FTimeOut = value; }
        }
        public List<TProduct> products
        {
            get { return getproducts(); }
        }
        private List<TProduct> getproducts()
        {
            List<TProduct> list = new List<TProduct>();
            list.Add(product1);
            list.Add(product2);
            if (product3 != null)
            {
                list.Add(product3);
                if (product4 != null)
                {
                    list.Add(product4);
                    if (product5 != null)
                        list.Add(product5);
                }
            }
            return list;
        }
        public TProduct product1
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FCity).Include(p=>p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId1);
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public TProduct product2
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId2);
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public TProduct product3
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId3);
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public TProduct product4
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId4);
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public TProduct product5
        {
            get
            {
                TProduct prod = _dbIgo.TProducts.Include(p => p.FSubCategory).FirstOrDefault(p => p.FProductId == FProductId5);
                if (prod != null)
                    return prod;
                return null;
            }
        }
        public virtual TProduct FProductId1Navigation { get; set; }
        public virtual TProduct FProductId2Navigation { get; set; }
        public virtual TProduct FProductId3Navigation { get; set; }
        public virtual TProduct FProductId4Navigation { get; set; }
        public virtual TProduct FProductId5Navigation { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }


    }
}
