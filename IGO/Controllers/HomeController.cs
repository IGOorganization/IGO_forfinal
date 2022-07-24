using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DemoIgoContext _IgoContext;

        public HomeController(ILogger<HomeController> logger, DemoIgoContext db)
        {
            _logger = logger;
            _IgoContext = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Home()
        {
            //string viewRecord = "";
            //List<ViewItems> list = null;
            //if (!HttpContext.Session.Keys.Contains(CDictionary.SK_瀏覽過的_商品們_列表))
            //{
            //    list = new List<ViewItems>();
            //}
            //else
            //{
            //    viewRecord = HttpContext.Session.GetString(CDictionary.SK_瀏覽過的_商品們_列表);
            //    list = JsonSerializer.Deserialize<List<ViewItems>>(viewRecord);
            //}
            //ViewItems items = new ViewItems()
            //{
            //    productName = "台北喜來登大飯店｜十二廚",
            //    productPhotoPath = "~/img/Home_HotSales_03.jpg"

            //};
            return View();
        }


        public IActionResult City()
        {
            return View();

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            TCustomer cust = _IgoContext.TCustomers.FirstOrDefault(n => n.FPhone == vModel.txtAccount);
            if (cust != null)
            {
                //if (cust.FPassword.Equals(vModel.txtPassword))
                //{
                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId);

                    return RedirectToAction("Home", "Home");
                //}
            }
            
            return View();
        }
        public IActionResult checkUser()
        {
            int userid = 0;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                return Json(userid);
            }
            return Json("尚未登入");
        }
    }
}
