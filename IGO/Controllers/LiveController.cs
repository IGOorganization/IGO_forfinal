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
    public class LiveController : Controller
    {
        DemoIgoContext _dbIgo;
        public LiveController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult getSupplier(int page)
        {
            IEnumerable<TSupplier> suppliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1);
            page--;
            List<TSupplier> list = new List<TSupplier>();
            for (int i = page * 9; i < (page + 1) * 9; i++)
            {
                if (i < suppliers.ToList().Count())
                {
                    list.Add(suppliers.ToList()[i]);
                }

            }
            string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(result);
        }
        public IActionResult searchbycity(int cityid)
        {
            List<TSupplier> list = new List<TSupplier>();
            if (cityid == 0)
            {
                IEnumerable<TSupplier> suppliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1);
                for (int i = 0; i < 9; i++)
                {
                    if (i < suppliers.ToList().Count())
                    {
                        list.Add(suppliers.ToList()[i]);
                    }
                }
            }
            else
            {
                IEnumerable<TSupplier> suppliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1 && n.FCityId == cityid);
                for (int i = 0; i < 9; i++)
                {
                    if (i < suppliers.ToList().Count())
                    {
                        list.Add(suppliers.ToList()[i]);
                    }
                }
            }
            string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(result);
        }
        public IActionResult pages(int cityid)
        {
            if (cityid != -1)
            {
                int p = _dbIgo.TSuppliers.Where(n => n.FCityId == cityid && n.FSubCategoryId == 1).Count();
                decimal result = Math.Ceiling((decimal)p / 9);
                return Json(result);
            }
            else
            {
                int p = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1).Count();
                decimal result = Math.Ceiling((decimal)p / 9);
                return Json(result);
            }
        }
        public IActionResult RoomList(int supplierid)
        {
            return View(_dbIgo.TSuppliers.FirstOrDefault(n => n.FSupplierId == supplierid));
        }
        public IActionResult getRoomType(int supid)
        {
            IEnumerable<TProduct> products = _dbIgo.TProducts.Where(n => n.FSupplierId == supid && n.FSubCategoryId == 1).ToList();
            List<CProductViewModel> list = new List<CProductViewModel>();
            foreach (TProduct t in products)
            {
                CProductViewModel product = new CProductViewModel(_dbIgo);
                product.product = t;
                list.Add(product);
            }

            string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(result);
        }
        public IActionResult gettickets(int prodid)
        {
            CProductViewModel product = new CProductViewModel(_dbIgo);
            product.product = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == prodid);
            return Json(product.tickets);
        }
        public IActionResult AddToCart(CToCart ToCart)
        {
            int userid = 0;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return Json("請先登入");
            }
            Random ran = new Random();
            string s = (ran.Next(1, 1000) * ran.Next(1, 1000)).ToString();
            try
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                TProduct prod = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == ToCart.ToCartId);
                CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                VMprod.product = prod;
                TShoppingCart cart = new TShoppingCart
                {
                    FProductId = (int)VMprod.FProductId,
                    FCustomerId = userid,
                    FTicketId = ToCart.fTickettype,
                    FQuantity = ToCart.fQuantity,
                    FTotalPrice = VMprod.tickets[0].price * ToCart.fQuantity,
                    FTempOrder = s,
                    FBookingTime = ToCart.fBookingTime,
                };
                _dbIgo.TShoppingCarts.Add(cart);

                _dbIgo.SaveChanges();
                return Json("加入成功");
            }
            catch
            {
                string result = System.Text.Json.JsonSerializer.Serialize("失敗");
                return Json(result);
            }
        }

        public IActionResult TakeSoldOut(int cid, int month, int ticketid)
        {
            CProductViewModel p = new CProductViewModel(_dbIgo);
            p.product = _dbIgo.TProducts.Find(cid);
            p.ticketid = ticketid;
            List<CSoldOut> items = new List<CSoldOut>();
            foreach (CSoldOut s in p.Solded)
            {
                int m = DateTime.Parse(s.Date).Month;
                if (m == month + 1)
                {
                    items.Add(s);
                }
            }
            return Json(items);
        }
        public IActionResult searchBykeyword(string keyword)
        {
            IEnumerable<TSupplier> suppliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1&&n.FCompanyName.Contains(keyword));
            
            List<TSupplier> list = new List<TSupplier>();
            for (int i = 0; i < 9; i++)
            {
                if (i < suppliers.ToList().Count())
                {
                    list.Add(suppliers.ToList()[i]);
                }

            }
            string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(result);
        }
    }
}
