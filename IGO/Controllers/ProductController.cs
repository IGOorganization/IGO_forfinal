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
    public class ProductController : Controller
    {
        private DemoIgoContext _dbIgo;
        public ProductController(DemoIgoContext db)
        {
            _dbIgo = db;
        }

        public IActionResult ProductsView(int id)
        {
            return View(_dbIgo.TSubCategories.Where(n => n.FSubCategoryId == id).FirstOrDefault());
        }

        //public IActionResult FoodView(int id)
        //{

        //    List<CProductViewModel> products = getProduct(id);
        //    return View(products);
        //}

        //public IActionResult ExhibitionView(int id)
        //{
        //    List<CProductViewModel> products = getProduct(id);
        //    return View(products);
        //}

        //public IActionResult ViewPointView(int id)
        //{
        //    List<CProductViewModel> products = getProduct(id);
        //    return View(products);
        //}

        public IActionResult getProduct(int subid, int page)
        {
            IEnumerable<TProduct> prod = _dbIgo.TProducts.Where(n => n.FSubCategoryId == subid);
            List<CProductViewModel> products = new List<CProductViewModel>();
            page--;
            for (int i = page * 9; i < (page + 1) * 9; i++)
            {
                if (i < prod.ToList().Count())
                {
                    CProductViewModel pvm = new CProductViewModel(_dbIgo);
                    pvm.product = prod.ToList()[i];
                    products.Add(pvm);
                }

            }
            string result = System.Text.Json.JsonSerializer.Serialize(products);
            return Json(result);
        }
        public IActionResult pages(int subid)
        {
            int p = _dbIgo.TProducts.Where(n => n.FSubCategoryId == subid).Count();
            decimal result = Math.Ceiling((decimal)p / 9);
            return Json(result);
        }
        public IActionResult SearchAndRank(int subid, int cityid)
        {
            IEnumerable<TProduct> prod = _dbIgo.TProducts.Where(n => n.FSubCategoryId == subid && n.FCityId == cityid);
            List<CProductViewModel> products = new List<CProductViewModel>();
            foreach (TProduct p in prod)
            {
                CProductViewModel pvm = new CProductViewModel(_dbIgo);
                pvm.product = p;
                products.Add(pvm);
            }
            string result = System.Text.Json.JsonSerializer.Serialize(products);
            return Json(result);
        }
        public IActionResult Detail(int prodid)
        {
            CProductViewModel product = new CProductViewModel(_dbIgo);
            product.product = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == prodid);
            return View(product);
        }

        public IActionResult AddToCart(CToCart ToCart)
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
                TProduct prod = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == ToCart.ToCartId);
                CProductViewModel VMprod = new CProductViewModel(_dbIgo);
                VMprod.product = prod;
                TShoppingCart cart = new TShoppingCart
                {
                    FProductId = (int)VMprod.FProductId,
                    FCustomerId = userid,
                    FTicketId = VMprod.tickets[ToCart.fTickettype].ticketid,
                    FQuantity = ToCart.fQuantity,
                    FTotalPrice = VMprod.tickets[ToCart.fTickettype].price * ToCart.fQuantity,
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
    }
}
