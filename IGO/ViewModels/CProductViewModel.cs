using IGO.Models;
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
        private TProduct _prod = new TProduct();
        public CProductViewModel(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public TProduct product
        {
            get
            {
                return _prod;
            }
            set
            {
                _prod = value;
            }
        }
        public int FProductId
        {
            get
            {
                return _prod.FProductId;
            }
            set
            {
                _prod.FProductId = value;
            }
        }
        [DisplayName("商品名稱")]
        public string FProductName
        {
            get
            {
                return _prod.FProductName;
            }
            set
            {
                _prod.FProductName = value;
            }
        }
        [DisplayName("商品名稱")]
        public int? FCityId
        {
            get
            {
                return _prod.FCityId;
            }
            set
            {
                _prod.FCityId = value;
            }
        }
        [DisplayName("商品名稱")]
        public string FAddress
        {
            get
            {
                return _prod.FAddress;
            }
            set
            {
                _prod.FAddress = value;
            }
        }
        [DisplayName("商品名稱")]
        public int? FSupplierId
        {
            get
            {
                return _prod.FSupplierId;
            }
            set
            {
                _prod.FSupplierId = value;
            }
        }
        [DisplayName("商品名稱")]
        public int? FQuantity
        {
            get
            {
                return _prod.FQuantity;
            }
            set
            {
                _prod.FQuantity = value;
            }
        }
        [DisplayName("商品名稱")]
        public string FStartTime
        {
            get
            {
                return _prod.FStartTime;
            }
            set
            {
                _prod.FStartTime = value;
            }
        }
        [DisplayName("下架時間")]
        public string FEndTime
        {
            get
            {
                return _prod.FEndTime;
            }
            set
            {
                _prod.FEndTime = value;
            }
        }
        [DisplayName("供應商")]
        public int? FSubCategoryId
        {
            get
            {
                return _prod.FSubCategoryId;
            }
            set
            {
                _prod.FSubCategoryId = value;
            }
        }
        [DisplayName("商品介紹")]
        public string FIntroduction
        {
            get
            {
                return _prod.FIntroduction;
            }
            set
            {
                _prod.FIntroduction = value;
            }
        }
        [DisplayName("是否打折")]
        public bool? FDiscountOrNot
        {
            get
            {
                return _prod.FDiscountOrNot;
            }
            set
            {
                _prod.FDiscountOrNot = value;
            }
        }

    }
}
