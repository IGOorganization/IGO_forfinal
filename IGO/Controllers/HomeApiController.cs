using IGO.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class HomeApiController : Controller
    {
        private readonly DemoIgoContext _IgoContext;   //注入
        private readonly IWebHostEnvironment _host;

        public HomeApiController(DemoIgoContext db, IWebHostEnvironment hostEnvironment)
        {
            _IgoContext = db;
            _host = hostEnvironment;
        }
        public IActionResult NewsCarousel() //最新消息廣告
        {
            return View();  //改成return Content() 用Ajax
        }
        public IActionResult CustomerCounts() //目前會員人數
        {
            return View();  //改成return Content() 用Ajax
        }
        public IActionResult HotPoducts(/*CHomeViewModel vModel*/) //IGO熱銷商品:按訂單數量排
        {
            //var hotProduct = (from o in _context.TOrderDetails
            //                  group o by o.FOrderDetailsId into g
            //                  orderby g.Count() descending
            //                  select g.Key).Take(3).ToList();

            //var hotProduct = _context.TOrderDetails.GroupBy(o => o.FOrderDetailsId).Select(o => new { Count = o.Count() }).Take(3).ToList();
            //var hotProduct = _context.TOrderDetails.GroupBy(o => o.FOrderDetailsId).Select(o => o.Key ).Take(3).ToList();
            //var hotProduct = _context.TOrderDetails.Include(o=>o.FProductId).GroupBy(o => o.FOrderDetailsId).Select(o => o.Key).Take(3).ToList();


            //return Json(hotProduct);
            return View();  //改成return Content() 用Ajax
        }
        public IActionResult HotCategory() //票卷種類:之後增加?
        {
            return View();  //改成return Content() 用Ajax
        }
        public IActionResult HostCity() //熱門城市排行:按訂單數量排 台北市 台中市 新北市
        {
            return View();  //改成return Content() 用Ajax
        }
        public IActionResult SpecialOffer() //限時優惠:父親節 十二廚
        {
            return View();  //改成return Content() 用Ajax
        }

        public IActionResult showViewProductCountBySession() //瀏覽過的商品_次數
        {
            int viewCount = 0;

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_瀏覽過的_次數))
            {
                viewCount++;
            }
            else
            {
                viewCount = (int)HttpContext.Session.GetInt32(CDictionary.SK_瀏覽過的_次數);
                viewCount++;
            }
            HttpContext.Session.SetInt32(CDictionary.SK_瀏覽過的_次數, viewCount);

            return Content(viewCount.ToString());
        }
        public IActionResult showViewNoProductCountBySession()
        {
            int viewCount = 0;

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_瀏覽過的_次數))
            {
                viewCount = 0;
            }
            else
            {
                viewCount = (int)HttpContext.Session.GetInt32(CDictionary.SK_瀏覽過的_次數);
            }
            HttpContext.Session.SetInt32(CDictionary.SK_瀏覽過的_次數, viewCount);

            return Content(viewCount.ToString());
        }
        //public IActionResult showViewProductContentBySession() //瀏覽過的商品_列表
        //{
        //    string jsonView = "";
        //    List<ViewItems> list = null;

        //    if (!HttpContext.Session.Keys.Contains(CDictionary.SK_瀏覽過的_商品們_列表))
        //    {
        //        list = new List<ViewItems>();
        //    }
        //    else
        //    {
        //        jsonView = HttpContext.Session.GetString(CDictionary.SK_瀏覽過的_商品們_列表);
        //        list = JsonSerializer.Deserialize<List<ViewItems>>(jsonView); //將字串轉為json

        //    }
        //    ViewItems item = new ViewItems()
        //    {
        //        productName = "普立茲新聞攝影獎80週年展",
        //        productPhotoPath = "Home_carousel_01.jpg"

        //    };
        //    list.Add(item);
        //    jsonView = JsonSerializer.Serialize(list); //json轉為字串
        //    HttpContext.Session.SetString(CDictionary.SK_瀏覽過的_商品們_列表, jsonView);

        //    return Content(item.productName);
        //    //return Json(jsonView);
        //}
    }
}
