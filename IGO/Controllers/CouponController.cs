using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CouponController : Controller
    {
        private DemoIgoContext _dbIgo;
        public CouponController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult List()
        {
            List<CouponViewModel> list = new List<CouponViewModel>();
            foreach(TCoupon c in _dbIgo.TCoupons.ToList())
            {
                CouponViewModel cou = new CouponViewModel(_dbIgo);
            
                cou.coupon = c;
                list.Add(cou);
            }
            return View(list);
        }
        public IActionResult Detail()
        {

            return View();
        }
        
    }
}
