using IGO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CCategoryViewModel
    {
        private TSubCategory _sb;
        private TCategory _cate;
        public CCategoryViewModel()
        {          
            _sb = new TSubCategory();
            _cate = new TCategory();
        }
        //TCategory
        public TCategory tCategory
        {
            get { return _cate; }
            set { _cate = value; }
        }
        public int CategoryId
        {
            get { return _cate.FCategoryId; }
            set { _cate.FCategoryId = value; }
        }
        public string CategoryName
        {
            get { return _cate.FCategoryName; }
            set { _cate.FCategoryName = value; }
        }
        //8/3新增
        public string CategoryPhotoPath
        {
            get { return _cate.FCategoryPhotoPath; }
            set { _cate.FCategoryPhotoPath = value; }
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
        
    }
}
