using IGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CustomerController : Controller
    {
        private DemoIgoContext _dbIgo;
        public CustomerController(DemoIgoContext db)
        {
            _dbIgo = db;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(TCustomer c)
        {

            _dbIgo.TCustomers.Add(c);
            _dbIgo.SaveChanges();
            return RedirectToAction("Home", "Home");

        }

        public IActionResult UserData()
        {
            int userid = 0;
            userid = (int) HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);

            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
                       
            return View(data);
        }
    }
}
