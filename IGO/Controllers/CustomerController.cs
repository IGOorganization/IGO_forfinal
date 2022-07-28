using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DemoIgoContext _dbIgo;
        public static TCustomer igoUser = null;
        public static int userid = 0;
        public static string userName = "";
        public static string imgpath = "";
        private IWebHostEnvironment _environment;
        public CustomerController(DemoIgoContext db, IWebHostEnvironment igo)
        {
            _dbIgo = db;
            _environment = igo;
        }

        public IActionResult List()
        {

            List<CCustomerViewModel> List = new List<CCustomerViewModel>();
            // 從DB裡拿出Customer的資料
            List<TCustomer> dbList = _dbIgo.TCustomers.ToList();

            //逐筆轉換為CCustomerViewModel
            foreach (TCustomer t in dbList)
            {
                CCustomerViewModel cc = new CCustomerViewModel();
                cc.customer = t;
                List.Add(cc);
            }

            return View(dbList);

        }

        //===================================================> 登入系統 <===========================================

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(n => n.FPhone == vModel.txtAccount);
            if (cust != null)
            {
                if (cust.FPassword.Equals(vModel.txtPassword))
                {
                   

                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId);
                    
                    userName = $"{cust.FLastName}" + $"{cust.FFirstName}";
                    userid = cust.FCustomerId;
                    imgpath = cust.FUserPhoto;

                    return RedirectToAction("Home", "Home");
                }
            }
            return RedirectToAction("Home", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            return RedirectToAction("Home", "Home");
        }

        public IActionResult checkUser()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return Json(userid);
            }
            return Json("尚未登入");
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

        //===================================================> 登入系統 <===========================================






        //===================================================> 會員資料管理 <===========================================

        public IActionResult Index()
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }

        public IActionResult UserData()
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }
        [HttpPost]
            public IActionResult UserData(CCustomerViewModel vmodel)
        {
            
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            if (data != null)
            {
                if(vmodel.photo != null)
                { 
                    string imgName = Guid.NewGuid().ToString() + ".jpg";
                    vmodel.photo.CopyTo(new FileStream(
                        _environment.WebRootPath + "/img/" + imgName, FileMode.Create));
                    data.FUserPhoto = imgName;
                }
                data.FLastName = vmodel.FLastName;
                data.FFirstName = vmodel.FFirstName;
                data.FPhone = vmodel.FPhone;
                data.FEmail = vmodel.FEmail;
                data.FAddress = vmodel.FAddress;
                data.FGender = vmodel.FGender;
            }
            _dbIgo.SaveChanges();
            return RedirectToAction("Index");
        }
            public IActionResult EditPWD ()
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditPWD(CCustomerViewModel vmodel)
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            if (data != null)
            {
                data.FPassword = vmodel.FPassword;
            }
            _dbIgo.SaveChanges();
            return View(data);
        }

        public IActionResult Delete( )
        {

            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            if (data != null)
            {
                _dbIgo.TCustomers.Remove(data);
                _dbIgo.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //===================================================> 會員資料管理 <=========================================== 
    }
}
