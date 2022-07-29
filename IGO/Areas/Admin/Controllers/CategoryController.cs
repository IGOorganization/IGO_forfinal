using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CategoryController : Controller
    {
        private readonly DemoIgoContext _IgoContext;
        public CategoryController(DemoIgoContext db)
        {
            _IgoContext = db;
        }
        public IActionResult List()   //列出主類別
        {
            var q = (from c in _IgoContext.TCategories
                     select c).ToList();

            CCategoryViewModel vModel = null;

            List<CCategoryViewModel> v = new List<CCategoryViewModel>();

            foreach (var r in q) 
            {
                vModel = new CCategoryViewModel()
                {
                    CategoryId = _IgoContext.TCategories.FirstOrDefault(m => m.FCategoryId == r.FCategoryId).FCategoryId,
                    CategoryName = _IgoContext.TCategories.FirstOrDefault(m => m.FCategoryId == r.FCategoryId).FCategoryName
                };
                v.Add(vModel);
            }

            return View(v);
        }
        public IActionResult CreateCategory(TCategory category, string categoryname) //新增主類別
        {

            category.FCategoryName = categoryname;


            _IgoContext.TCategories.Add(category);
            _IgoContext.SaveChanges();

            return RedirectToAction("List");
        }
        public IActionResult DeleteCategory(string categoryID) //刪除商品主分類 //宜潔7/29新增
        {
            string id = categoryID.Split("-")[1];
            int CategoryID = Convert.ToInt32(id);

            TCategory c = _IgoContext.TCategories.FirstOrDefault(c => c.FCategoryId == CategoryID);

            if (c != null) 
            {
                _IgoContext.TCategories.Remove(c);
                _IgoContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        //查詢商品指定次分類數量:宜潔7/29新增
        public IActionResult querySubcategoryCount(TSubCategory Subcategory, string categoryID) //查詢subcategoryId
        {
            string id = categoryID.Split("-")[1];
            int CategoryID = Convert.ToInt32(id);

            int count = _IgoContext.TSubCategories.Where(c => c.FCategoryId == CategoryID).Count();

            return Content(count.ToString());
        }


        public IActionResult ShowSubcategory(string categoryID) //顯示次類別
        {
            string id = categoryID.Split("-")[1];
            int CategoryID = Convert.ToInt32(id);

            var subcategory = from s in _IgoContext.TSubCategories
                    where s.FCategoryId == CategoryID
                    select s;


            return Json(subcategory);
        }
        //新增商品次分類    //新增subcategory:宜潔7/29新增
        public IActionResult CreateSubcategory(TSubCategory Subcategory, string Subcategoryname, int CategoryID) //新增次類別
        {

            Subcategory.FSubCategoryName = Subcategoryname;
            Subcategory.FCategoryId = CategoryID;

            _IgoContext.TSubCategories.Add(Subcategory);
            _IgoContext.SaveChanges();

            return RedirectToAction("List");
        }
        //查詢商品次分類id:宜潔7/29新增
        public IActionResult querySubcategoryId(TSubCategory Subcategory, string Subcategoryname) //查詢subcategoryId
        {

            int SubCategoryId = _IgoContext.TSubCategories.FirstOrDefault(c => c.FSubCategoryName == Subcategoryname).FSubCategoryId;

            return Content(SubCategoryId.ToString());
        }

        //刪除商品次分類 //宜潔7/29新增
        public IActionResult deleteSubcategory(int SubcategoryId) //查詢subcategoryId
        {
            TSubCategory s = _IgoContext.TSubCategories.FirstOrDefault(c => c.FSubCategoryId == SubcategoryId);

            if (s != null)
            {
                _IgoContext.TSubCategories.Remove(s);
                _IgoContext.SaveChanges();
            }
            return RedirectToAction("List");

        }
    }
}
