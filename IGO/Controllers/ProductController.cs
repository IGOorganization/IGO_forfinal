using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult IndexTotal(int City,int ProductID)
        {
            return View();
        }

        //public string TitleList(int fsubCategoryId)
        //{
        //    DemoIgoContext db = new DemoIgoContext();
        //    var q = from p in db.TSubCategories
        //            where p.FSubCategoryId == fsubCategoryId
        //            select p;
        //    string a = q.ToString();
        //    return a;
        //}
    }
}
