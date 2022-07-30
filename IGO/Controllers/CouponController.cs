using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Http;
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
        static int i;
        public IActionResult List()
        {
            List<CouponViewModel> list = new List<CouponViewModel>();
            foreach (TCoupon c in _dbIgo.TCoupons.ToList())
            {
                CouponViewModel cou = new CouponViewModel(_dbIgo);
                cou.coupon = c;
                list.Add(cou);
            }
            return View(list);
        }
        public IActionResult detail(int id)
        {
            i++;
            CouponViewModel coup = new CouponViewModel(_dbIgo);
            coup.coupon = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == id);
            return View(coup);
        }
        public IActionResult TakeSoldOut(int cid, int month)
        {
            CouponViewModel c = new CouponViewModel(_dbIgo);
            c.coupon = _dbIgo.TCoupons.Find(cid);
            List<CSoldOut> items = new List<CSoldOut>();
            foreach (CSoldOut s in c.Solded)
            {
                int m = DateTime.Parse(s.Date).Month;
                if (m == month + 1)
                {
                    items.Add(s);
                }
            }
            return Json(items);
        }
        public IActionResult AddToCart(CToCart c)
        {
            int userid = 0;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                //string result = System.Text.Json.JsonSerializer.Serialize("請先登入");
                return Json("請先登入");
            }
            Random ran = new Random();
            string s = (ran.Next(1, 1000) * ran.Next(1, 1000)).ToString();
            try
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                CouponViewModel cvm = new CouponViewModel(_dbIgo);
                cvm.coupon = _dbIgo.TCoupons.FirstOrDefault(n => n.FCouponId == c.ToCartId);
                if (c.fTickettype == 1)
                {
                    int d = Convert.ToInt32(cvm.FDiscount);
                    foreach (CProductViewModel t in cvm.VMproducts)
                    {
                        TShoppingCart cart = new TShoppingCart
                        {
                            FProductId = t.FProductId,
                            FCustomerId = userid,
                            FTicketId = _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FProductId == t.FProductId).FTicketId,
                            FQuantity = c.fQuantity,
                            FTotalPrice = (_dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FProductId == t.FProductId).FPrice) * c.fQuantity * d / 100,
                            FTempOrder = s,
                            FBookingTime = c.fBookingTime,
                            FCouponId = c.ToCartId
                        };
                        _dbIgo.TShoppingCarts.Add(cart);
                    }
                }
                if (c.fTickettype == 2)
                {
                    foreach (CProductViewModel t in cvm.VMproducts)
                    {
                        TShoppingCart cart = new TShoppingCart
                        {
                            FProductId = t.FProductId,
                            FCustomerId = userid,
                            FTicketId = _dbIgo.TTicketAndProducts.Where(n => n.FProductId == t.FProductId).ToList()[1].FTicketId,
                            FQuantity = c.fQuantity,
                            FTotalPrice = (_dbIgo.TTicketAndProducts.Where(n => n.FProductId == t.FProductId).ToList()[1].FPrice) * c.fQuantity,
                            FTempOrder = s,
                            FBookingTime = c.fBookingTime,
                            FCouponId = c.ToCartId
                        };
                        _dbIgo.TShoppingCarts.Add(cart);
                    }
                }
                _dbIgo.SaveChanges();
                return Json("加入成功");
            }
            catch
            {
                string result = System.Text.Json.JsonSerializer.Serialize("失敗");
                return Json(result);
            }
        }
    }
}
