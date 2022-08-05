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
using System.Net.Mail;
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
        public int Ad_Sup_id = 215;
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
                if (cust.FPassword.Equals(vModel.txtPassword) && cust.FSupplierId == Ad_Sup_id)
                {
                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId); ;
                    userid = cust.FCustomerId;
                    userName = $"{cust.FLastName}" + "" + $"{cust.FFirstName}";
                    //Welcomeuser = userName + "您好，歡迎使用 IGO";
                    imgpath = cust.FUserPhoto;
                    return Content("admin", "text/plain", Encoding.UTF8);
                }
                else if (cust.FPassword.Equals(vModel.txtPassword))
                {
                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId); ;
                    userid = cust.FCustomerId;
                    userName = $"{cust.FLastName}" + "" + $"{cust.FFirstName}";
                    //Welcomeuser = userName + "您好，歡迎使用 IGO";
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


        //public IActionResult LoginBackEnd()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult LoginBackEnd(CLoginViewModel vModel)
        //{
        //    if (vModel.txtAccount == null || vModel.txtAccount == null)
        //    {
        //        return Content("empty", "text/plain", System.Text.Encoding.UTF8);
        //    }

        //    TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(n => n.FPhone == vModel.txtAccount);
        //    if (cust != null)
        //    {
        //        if (cust.FPassword.Equals(vModel.txtPassword))
        //        {
        //            HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId); ;
        //            userid = cust.FCustomerId;
        //            userName = $"{cust.FLastName}" + "" + $"{cust.FFirstName}";
        //            imgpath = cust.FUserPhoto;

        //            return RedirectToAction("Home", "Home");
        //        }
        //    }
        //    return RedirectToAction("Home", "Home");
        //}

        //===================================================> 登入系統 <===========================================

        public IActionResult MailforgetPwd(string Phone, string Email)
        {
            var customer = _dbIgo.TCustomers.FirstOrDefault(t => t.FPhone == Phone && t.FEmail == Email);

            if (customer != null)
            {
                //----------------------------------------------------------寄信------------------------------------------
                MailMessage em = new MailMessage();
                // 發信來源,最好與你發送信箱相同,否則容易被其他的信箱判定為垃圾郵件.
                em.From = new System.Net.Mail.MailAddress("igocompanysender@gmail.com");
                // 收件人 Email 地址
                em.To.Add("igocompanysender@gmail.com");
                // 主旨
                em.SubjectEncoding = System.Text.Encoding.UTF8;
                em.BodyEncoding = System.Text.Encoding.UTF8;
                em.Subject = "【會員修改密碼驗證信】IGO ticket shop";
                // 內文

                em.Body = "<html><body>" +
           "<h1 style='color:lightsalmon;background-color:black'>IGO</h1>" +
           "<hr>" +
           "<h3>【會員修改密碼驗證信】</h3>" +
           $"<h4>顧客:{customer.FFirstName}  先生/小姐</h4>" +
           "<br>" +
           "<h4 style='color:orange'>請點擊下面連結，進行密碼修改</h4>" +
           $"<h5> {Request.Scheme}://{Request.Host}/Customer/ForgetPWD?id={customer.FCustomerId} <h5>" +
           "<h4 style='color:white;background-color:gray'>IGO 版權所有©Copyright 2022 All Rights Reserved</h5>" +
           "</body></html>";

                // 內文是否為 HTML
                em.IsBodyHtml = true;
                // 優先權
                em.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("igocompanysender@gmail.com", "fxlijfrpaulssvln");

                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;

                try
                {
                    // 寄送出去
                    client.Send(em);
                }
                catch
                {

                }
                client.Dispose();

                return Json("寄送成功，");
            }
            return Json("資料有誤");
        }




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

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == vmodel.FCustomerId);
            try
            {
                if (cust != null) //符合驗證
                {
                    cust.FLastName = vmodel.FLastName;
                    cust.FFirstName = vmodel.FFirstName;
                    cust.FCityId = vmodel.FCityId;
                    cust.FEmail = vmodel.FEmail;
                    cust.FAddress = vmodel.FAddress;
                    cust.FGender = vmodel.FGender;
                    cust.FBirth = vmodel.FBirth;
                    _dbIgo.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch
            {
                return Json(false);
            }
        }

        public IActionResult EditImg()
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditImg(int customerID, IFormFile photo)
        {
            try
            {
                TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == customerID);
                if (data != null)
                {
                    if (photo != null)
                    {
                        string imgName = Guid.NewGuid().ToString() + ".jpg";
                        photo.CopyTo(new FileStream(
                            _environment.WebRootPath + "/img/" + imgName, FileMode.Create));
                        data.FUserPhoto = imgName;
                    }
                }
                _dbIgo.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }

        public IActionResult EditPWD()
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditPWD(CCustomerViewModel vModel)
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            if (data != null)
            {
                if (data.FPassword == vModel.FPassword)
                {
                    return Json("修改失敗，與舊的密碼相同");
                }
                else
                {
                    data.FPassword = vModel.FPassword;
                    _dbIgo.SaveChanges();
                }
            }
            return Json(true);
        }

        public IActionResult ForgetPWD(int id)
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult ForgetPWD(int customerid, string FPassword)
        {
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == customerid);
            if (data != null)
            {
                if (FPassword == data.FPassword)
                {
                    return Json("修改失敗，與舊的密碼相同");
                }
                else
                {
                    data.FPassword = FPassword;
                    _dbIgo.SaveChanges();
                }
            }
            return Json(true);
        }


        public IActionResult Delete()
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
