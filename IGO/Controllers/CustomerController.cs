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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DemoIgoContext _dbIgo;
        public static TCustomer igoUser = null;
        public static int userid = 0;
        public static string Welcomeuser = "";
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
            if (vModel.txtAccount == null || vModel.txtAccount == null)
            {
                return Content("empty", "text/plain", System.Text.Encoding.UTF8);
            }

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(n => n.FPhone == vModel.txtAccount);
            if (cust != null)
            {
                if (cust.FPassword.Equals(vModel.txtPassword))
                {
                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId); ;
                    userid = cust.FCustomerId;
                    userName = $"{cust.FLastName}" + "" + $"{cust.FFirstName}";
                    Welcomeuser = userName + "您好，歡迎使用 IGO";
                    imgpath = cust.FUserPhoto;

                    return Content("success", "text/plain", Encoding.UTF8);
                }
            }
            return Content("false", "text/plain", Encoding.UTF8);
        }

        public IActionResult Logout() //登出
        {
            HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            userName = "登入/ 註冊IGO";
            Welcomeuser = "您尚未登入IGO";
            return RedirectToAction("Home", "Home");
        }

        public IActionResult checkUser() 
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return Json(Welcomeuser);
            }
            return Json(Welcomeuser);
        }
    

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(TCustomer tc)
        {
            if (tc.FPhone == null || tc.FPassword == null || tc.FLastName == null ||
                tc.FFirstName == null || tc.FEmail == null)
            {
                return Content("欄位空白", "text/plain", System.Text.Encoding.UTF8);
            }

            else
            {
                var phone = _dbIgo.TCustomers.FirstOrDefault(t => t.FPhone == tc.FPhone);
                var email = _dbIgo.TCustomers.FirstOrDefault(t => t.FEmail == tc.FEmail);

                if (phone != null)
                {
                    return Content("此電話已有人使用", "text/plain", System.Text.Encoding.UTF8);
                }
                else if (email != null)
                {
                    return Content("此信箱已有人使用", "text/plain", System.Text.Encoding.UTF8);
                }
                else //輸入的值資料庫沒有
                {
                    _dbIgo.TCustomers.Add(tc);
                    _dbIgo.SaveChanges();
                    return Content("註冊成功", "text/plain", System.Text.Encoding.UTF8);
                }

            }
        }
        //===== 取得資料作驗證 =====
        public IActionResult getAccount([Bind("txtAccount")] TCustomer tc)
        {
            var cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FPhone == tc.FPhone);
            if (cust == null)
            {
                return Content("true", "text/plain", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("false", "text/plain", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult getEmail([Bind("txtEmail")] TCustomer tc)
        {
            var cust = _dbIgo.TCustomers.FirstOrDefault(m => m.FEmail == tc.FEmail);
            if (cust != null)
            {
                return Content("true", "text/plain", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("false", "text/plain", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult getPassword([Bind("txtEmail,fPassword")] TCustomer tc)
        {
            var cust = _dbIgo.TCustomers.FirstOrDefault(m => m.FEmail == tc.FEmail);
            if (tc.FPassword != null && cust != null)
            {
                if (cust.FPassword == tc.FPassword)
                {
                    return Content("true", "text/plain", System.Text.Encoding.UTF8);
                }
                else
                {
                    return Content("false", "text/plain", System.Text.Encoding.UTF8);
                };
            }
            else
            {
                return Content("false2", "text/plain", System.Text.Encoding.UTF8);
            }

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
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
                return View(data);
            }
        }
        [HttpPost]
            public IActionResult UserData(CCustomerViewModel vmodel)
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);

                if (cust != null && ModelState.IsValid) //符合驗證
                {
                    if (vmodel.photo != null)
                    {
                        string imgName = Guid.NewGuid().ToString() + ".jpg";
                        vmodel.photo.CopyTo(new FileStream(
                            _environment.WebRootPath + "/img/" + imgName, FileMode.Create));
                        cust.FUserPhoto = imgName;
                    }
                    cust.FLastName = vmodel.FLastName;
                    cust.FFirstName = vmodel.FFirstName;
                    cust.FPhone = vmodel.FPhone;
                    cust.FEmail = vmodel.FEmail;
                    cust.FAddress = vmodel.FAddress;
                    cust.FGender = vmodel.FGender;
                }
                _dbIgo.SaveChanges();
                cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
                string json = JsonSerializer.Serialize(cust.FCustomerId);
                HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId);

                return View(cust);
            }
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
