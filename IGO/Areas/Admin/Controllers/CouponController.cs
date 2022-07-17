using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CouponController : Controller
    {
        DemoIgoContext _dbIgo;
        public CouponController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult List()
        {
            List<CouponViewModel> list = new List<CouponViewModel>();
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
            return Json(coupons);
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
            return Json(coupons);
        }
        public IActionResult Edit(TCoupon cou)
        {
            List<CouponViewModel> coupons = new List<CouponViewModel>();
            TCoupon c = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == cou.FCouponId);
            c.FCouponName = cou.FCouponName;
            c.FDeadTime = cou.FDeadTime;
            c.FDiscount = cou.FDiscount;
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
            _dbIgo.SaveChanges();
            foreach (TCoupon coupon in _dbIgo.TCoupons)
            {
                CouponViewModel coup = new CouponViewModel(_dbIgo);
                coup.coupon = coupon;
                coupons.Add(coup);
            }
            return Json(coupons);
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
