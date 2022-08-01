using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class NewsletterController : Controller
    {
        private IWebHostEnvironment _environment;
        public NewsletterController(IWebHostEnvironment p)
        {
            _environment = p;
        }
        public IActionResult News()
        {
            return View();
        }

        //public IActionResult SendMail(string emails, string title, string content, string paths)
        public IActionResult SendMail()  //寄送電子報
        {
            //string body = string.Empty;
            ////using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            ////{
            ////    body = reader.ReadToEnd();
            ////}
            //using (StreamReader reader = new StreamReader(_environment.WebRootPath + "/txtfile/" + "emailtest.html"))
            //{
            //    body = reader.ReadToEnd();
            //}

            MailMessage mail = new MailMessage();
            // 發信來源,最好與你發送信箱相同,否則容易被其他的信箱判定為垃圾郵件.
            mail.From = new MailAddress("igocompanysender@gmail.com");
            // 收件人 Email 地址
            mail.To.Add("igocompanysender@gmail.com");
            // 主旨
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "IGO電子報";
            // 內文
            //mail.Body = body;
            mail.Body = "<html><body><img src='~/img/newsletter.jpg' /></body></html>";


            // 內文是否為 HTML
            mail.IsBodyHtml = true;
            // 優先權
            mail.Priority = MailPriority.Normal;

            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("igocompanysender@gmail.com", "fxlijfrpaulssvln"); //7/29宜潔更新金鑰

            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;


            //附帶檔案方法1:
            //Attachment attachment = new Attachment(@"\wwwroot\txtfile\newsletter.jpg");//<-這是附件部分~先用附件的物件把路徑指定進去~
            //attachment.ContentId = "Pic1";
            //attachment.ContentDisposition.Inline = true;
            //mail.Attachments.Add(attachment);
            //var htmlBody = "<img src=cid:Pic1/>";
            ////mail.Attachments.Add(attachment);

            //附帶檔案方法2:
            var attachment = new LinkedResource(_environment.WebRootPath+"/img/newsletter.jpg", MediaTypeNames.Image.Jpeg);//<-這是附件部分~先用附件的物件把路徑指定進去~

            attachment.ContentId = "Pic1";
            //attachment.ContentDisposition.Inline = True;

            var htmlBody = "<html><body><img src='cid:Pic1'/></body></html>";
            var altView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            altView.LinkedResources.Add(attachment);

            mail.AlternateViews.Add(altView);

            try
            {
                // 寄送出去
                client.Send(mail);


            }
            catch
            {

            }
            client.Dispose();
            return View();
        }
        public IActionResult Newspaper()  //電子報頁面
        {

            return PartialView();
        }
    }
}
