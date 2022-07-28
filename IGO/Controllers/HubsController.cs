using IGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class HubsController : Controller
    {
        private readonly DemoIgoContext _db;
        public HubsController(DemoIgoContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            String UserName = (_db.TCustomers.FirstOrDefault(c => c.FCustomerId == (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER))).FFirstName;
             ViewBag.UserName = UserName;
            return View();
        }
        public IActionResult getUserName()
        {
            String UserName = null;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                UserName = (_db.TCustomers.FirstOrDefault(c => c.FCustomerId == (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER))).FFirstName;
            }

            return Json(UserName);
        }
    }
}
