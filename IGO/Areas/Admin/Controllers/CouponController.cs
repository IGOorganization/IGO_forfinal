using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CouponController : Controller
    {
        private DemoIgoContext _dbIgo;
        private IWebHostEnvironment _enviroment;
        public CouponController(DemoIgoContext db,IWebHostEnvironment p)
        {
            _dbIgo = db;
            _enviroment = p;
        }
        public IActionResult List()
        {
            List<CouponViewModel> list = new List<CouponViewModel>();
            IEnumerable<TCoupon> datas = _dbIgo.TCoupons;
            foreach (TCoupon c in _dbIgo.TCoupons)
            {
                CouponViewModel coupon = new CouponViewModel(_dbIgo);
                coupon.coupon = c;
                list.Add(coupon);
            }
            return View(list);
        }
        public IActionResult Create(TCoupon cou)
        {
            List<CouponViewModel> coupons = new List<CouponViewModel>();
            _dbIgo.TCoupons.Add(cou);
            _dbIgo.SaveChanges();
            CouponViewModel coupon = new CouponViewModel(_dbIgo);
            coupon.coupon = _dbIgo.TCoupons.OrderBy(n => n.FCouponId).Last();
            coupons.Add(coupon);
            string result = System.Text.Json.JsonSerializer.Serialize(coupons);
            return Json(result);
        }
        public IActionResult Delete(int id)
        {
            List<CouponViewModel> coupons = new List<CouponViewModel>();
            TCoupon c = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == id);
            _dbIgo.TCoupons.Remove(c);
            _dbIgo.SaveChanges();
            foreach (TCoupon cou in _dbIgo.TCoupons)
            {
                CouponViewModel coup = new CouponViewModel(_dbIgo);
                coup.coupon = cou;
                coupons.Add(coup);
            }
            string result = System.Text.Json.JsonSerializer.Serialize(coupons);
            return Json(result);
        }
        public IActionResult Edit(TCoupon cou,IFormFile Photo)
        {
            TCoupon c = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == cou.FCouponId);
            c.FCouponName = cou.FCouponName;
            c.FDeadTime = cou.FDeadTime;
            c.FDiscount = cou.FDiscount;
            c.FQuantity = cou.FQuantity;
            c.FProductId1 = cou.FProductId1;
            c.FProductId2 = cou.FProductId2;
            if (cou.FProductId3 != null)
            {
                c.FProductId3 = cou.FProductId3;
                if (cou.FProductId4 != null)
                {
                    c.FProductId4 = cou.FProductId4;
                    if (c.FProductId5 != null)
                        c.FProductId5 = cou.FProductId5;
                }
            }
            c.FTimeOut = cou.FTimeOut;
            if (Photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Photo.CopyTo(new FileStream(_enviroment.WebRootPath + "/img/" + pName, FileMode.Create));
                c.FCouponImage = pName;
            }
            _dbIgo.SaveChanges();
            
            //回傳商品列表===============
            List<CouponViewModel> coupons = new List<CouponViewModel>();
            CouponViewModel coupon = new CouponViewModel(_dbIgo);
            coupon.coupon = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId==c.FCouponId);
            coupons.Add(coupon);
            string result = System.Text.Json.JsonSerializer.Serialize(coupons);
            return Json(result);
        }
        public IActionResult searchCouponById(int id)
        {
            TCoupon coupon = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == id);
            if (coupon != null)
            {
                CouponViewModel c = new CouponViewModel(_dbIgo);
                c.coupon = coupon;
                return Json(c);
            }
            return Json(null);
        }
    }
}
