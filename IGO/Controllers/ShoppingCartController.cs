using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private DemoIgoContext _dbIgo;

        public ShoppingCartController(IWebHostEnvironment hostEnvironment, DemoIgoContext db)
        {

            webHostEnvironment = hostEnvironment;
            _dbIgo = db;
        }
        public IActionResult List()
        {
            List<CShoppingCartViewModel> lists = new List<CShoppingCartViewModel>();

            foreach (TShoppingCart data in _dbIgo.TShoppingCarts.ToList())
            {
                CShoppingCartViewModel shoppingCartViewModel = new CShoppingCartViewModel(_dbIgo);
                shoppingCartViewModel.shoppingCart = data;
                lists.Add(shoppingCartViewModel);
            };


            return View(lists);
        }


        public JsonResult Delete(int id)
        {
            try
            {
                var shoppingCartItem = _dbIgo.TShoppingCarts.FirstOrDefault(s => s.FShoppingCartId == id);
                _dbIgo.TShoppingCarts.Remove(shoppingCartItem);
                _dbIgo.SaveChanges();
                return Json(new { Flag = true, Description = "刪除成功" });
            }
            catch (Exception)
            {
                return Json(new { Flag = false, Description = "刪除失敗" });
            }
        }
        public JsonResult Checked([FromBody] List<int> id)
        {
            HttpContext.Session.SetString(CDictionary.SK_Selected_Item, JsonSerializer.Serialize(id));

            return Json(true);
        }

        public IActionResult Pay()
        {
            var json = HttpContext.Session.GetString(CDictionary.SK_Selected_Item);
            List<int> IDs = JsonSerializer.Deserialize<List<int>>(json);

            List<CShoppingCartViewModel> lists = new List<CShoppingCartViewModel>();

            foreach (TShoppingCart data in _dbIgo.TShoppingCarts.Where(c => IDs.Contains(c.FShoppingCartId)).ToList())
            {
                CShoppingCartViewModel shoppingCartViewModel = new CShoppingCartViewModel(_dbIgo);
                shoppingCartViewModel.shoppingCart = data;
                lists.Add(shoppingCartViewModel);
            };

            return View(lists);
        }



        public IActionResult Finish()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("123", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap icon = new Bitmap(@"wwwroot\ShoppingCartImgs\IGO.jpg");
            Bitmap icon = new Bitmap(webHostEnvironment.WebRootPath + "/ShoppingCartImgs/IGO.jpg");
            Bitmap qrCodeImage = qrCode.GetGraphic(5, Color.Black, Color.White, icon, 15, 0);

            string outputFileName = @"wwwroot\img\Code.jpg";
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create/*, FileAccess.ReadWrite*/))
                {
                    qrCodeImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }






            //----------------------------------------------------------寄信------------------------------------------



            MailMessage em = new MailMessage();
            em.From = new System.Net.Mail.MailAddress("zeroqazggc0504@gmail.com");
            em.To.Add("zeroqazggc0504@gmail.com");
            em.SubjectEncoding = System.Text.Encoding.UTF8;
            em.BodyEncoding = System.Text.Encoding.UTF8;
            em.Subject = "IGO訂單已成立";
            //em.Body = "<html><body><img src='~/ShoppingCartImgs/野柳海洋世界.jpg'></body></html>";
            em.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("zeroqazggc0504@gmail.com", "ccpapemvmokdjpjk");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            //------------------------------------------------------夾帶檔案
            //var res = new LinkedResource(@"wwwroot\img\Code.jpg", MediaTypeNames.Image.Jpeg);
            var res = new LinkedResource(webHostEnvironment.WebRootPath + "/img/Code.jpg", MediaTypeNames.Image.Jpeg);
            res.ContentId = "Pic1";  //每個檔案都需要有一個contentID

            var htmlBody = "<html><body><h1>你好</h1><img src='cid:Pic1'></body></html>";

            var altView = AlternateView.CreateAlternateViewFromString(
                htmlBody, null, MediaTypeNames.Text.Html);

            altView.LinkedResources.Add(res);
            em.AlternateViews.Add(altView);

            try
            {
                client.Send(em);
            }
            catch
            {

            }
            return View();
        }

    }
}
