using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class OrderController : Controller
    {

        private readonly DemoIgoContext _IgoContext;  //注入
        public OrderController(DemoIgoContext db)
        {
            _IgoContext = db;
        }
        public IActionResult List()
        {

            var q = (from o in _IgoContext.TOrders
                     select o).ToList();

            COrdersViewModel vModel = null;
            List<COrdersViewModel> v = new List<COrdersViewModel>();

            foreach (var r in q)
            {
                vModel = new COrdersViewModel
                {
                    OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderId,
                    CustomerId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomerId,
                    LastName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomer.FLastName,
                    FirstName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomer.FFirstName,
                    OrderDate = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderDate,
                    StatusId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatusId,
                    TotalPrice = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FTotalPrice,
                    StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatus.FStatusName
                };
                v.Add(vModel);
            }
            return View(v);
        }        
        public IActionResult QueryByCancelOrder(int id) //訂單狀態查詢
        {
            int statusID = id;

            var q = (from o in _IgoContext.TOrders
                     where o.FStatusId == statusID
                     select o).ToList();
            COrdersViewModel vModel = null;
            List<COrdersViewModel> v = new List<COrdersViewModel>();

            foreach (var r in q)
            {
                vModel = new COrdersViewModel
                {
                    OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderId,
                    CustomerId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomerId,
                    LastName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomer.FLastName,
                    FirstName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == r.FOrderId).FCustomer.FFirstName,
                    OrderDate = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderDate,
                    StatusId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatusId,
                    TotalPrice = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FTotalPrice,
                    StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatus.FStatusName
                };
                v.Add(vModel);
            }
            return Json(v);
        }
        public IActionResult EditByOrderID(string Orderid)  //編輯訂單狀態
        {
            string id = Orderid.Split("-")[1];
            int orderID = Convert.ToInt32(id);

            COrdersViewModel vModel = null;

                vModel = new COrdersViewModel
                {
                    OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == orderID).FOrderId,
                    StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == orderID).FStatus.FStatusName
                };
               
            //return Content(orderID.ToString());
            return Json(vModel);
        }
        public IActionResult CancelEmailInfo(int Orderid)  //編輯訂單狀態
        {          
            var q = (from o in _IgoContext.TOrderDetails
                     where o.FOrderId == Orderid
                     select o).ToList();

            COrdersViewModel vModel = null;
            List<COrdersViewModel> v = new List<COrdersViewModel>();
            foreach (var r in q)
            {
                vModel = new COrdersViewModel
                {
                    OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == Orderid).FOrderId,
                    StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == Orderid).FStatus.FStatusName,
                    LastName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == Orderid).FCustomer.FLastName,
                    FirstName = _IgoContext.TOrders.Include(m => m.FCustomer).FirstOrDefault(m => m.FOrderId == Orderid).FCustomer.FFirstName,
                    TotalPrice = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == Orderid).FTotalPrice,
                    OrderDetailsId = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FOrderDetailsId,
                    Price = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FPrice,
                    ProductName = _IgoContext.TOrderDetails.Include(m => m.FProduct).FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FProduct.FProductName,
                    Quantity = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FQuantity,
                    TicketName = _IgoContext.TOrderDetails.Include(m => m.FTicket).FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FTicket.FTicketName                  
                };
                v.Add(vModel);
            }
            //return Content(orderID.ToString());
            return Json(v);
        }

        public IActionResult UpdateByOrderID(int Orderid, int Statusid)  //更新訂單狀態
        {

            var order = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == Orderid);

            if (order != null)
            {
                order.FStatusId = Statusid;
            }
            _IgoContext.SaveChanges();
            return RedirectToAction("List");

        }

        public IActionResult SentCancelOrderEmail(string lastname, string firstname, decimal totalprice)
        //public IActionResult SentCancelOrderEmail(string[] cancelinfo)
        {
            //Debug.WriteLine(cancelinfo);
            //for (int i = 0; i < cancelinfo.Length; i++) 
            //{
            //    cancelinfo[i]
            //}

            DateTime today = DateTime.Now.Date;

            MailMessage mail = new MailMessage();
            // 發信來源,最好與你發送信箱相同,否則容易被其他的信箱判定為垃圾郵件.
            mail.From = new MailAddress("igocompanysender@gmail.com");
            // 收件人 Email 地址
            mail.To.Add("igocompanysender@gmail.com");
            // 主旨
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "【顧客取消訂單】IGO ticket shop";
            // 內文
            //mail.Body = body;
            //mail.Body = "<html><body>" +
            //"<h1 style='color:lightsalmon;background-color:black'>IGO</h1>" +
            //"<hr>" +
            //"<h4>【顧客取消訂單】</h4>" +
            //"<h5>顧客: 戴樂古 先生/小姐</h5>" +
            //"<h5>取消訂單編號:236</h5>" +
            //"<h5>訂單狀態: 取消訂單</h5>" +
            //"<h5>退款狀態: 申請退款中</h5>" +
            //"<h5>產品名稱: 新竹芙洛麗大飯店FLEURLIS |食譜自助百匯</h5>" +
            //"<h5>數量: 1張</h5>" +
            //"<h5>票種: 下午茶</h5>" +
            //"<h5>金額: $911</h5>" +
            //"<br>" +
            //"<h4 style='color:white;background-color:gray'>IGO 版權所有©Copyright 2022 All Rights Reserved</h4>" +
            //"</body></html>";

            mail.Body = "<html><body>" +
            "<h1 style='color:lightsalmon;background-color:black'>IGO</h1>" +
            "<hr>" +
            "<h3>【顧客取消訂單】</h3>" +
            $"<h4>顧客:{lastname}{firstname}  先生/小姐</h4>" +
            $"<h4>於{today}取消訂單<h4>" +
            "<h4>訂單狀態: 取消訂單</h4>" +
            "<h4>退款狀態: 申請退款中</h4>" +          
            $"<h4>金額:{totalprice}元</h4>" +
            "<br>" +
            "<h4 style='color:orange'> 詳細取消訂單內容可官網以下進入'我的訂單'查詢</h4>" +
            "<a href='/Home/Home'>我的訂單</a>" +
            "<h4 style='color:white;background-color:gray'>IGO 版權所有©Copyright 2022 All Rights Reserved</h5>" +
            "</body></html>";

            // 內文是否為 HTML
            mail.IsBodyHtml = true;
            // 優先權
            mail.Priority = MailPriority.Normal;

            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("igocompanysender@gmail.com", "eklktfcbelgblutv");

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
            //var attachment = new LinkedResource(@"wwwroot\txtfile\newsletter.jpg", MediaTypeNames.Image.Jpeg);//<-這是附件部分~先用附件的物件把路徑指定進去~

            //attachment.ContentId = "Pic1";
            ////attachment.ContentDisposition.Inline = True;

            //var htmlBody = "<html><body><img src='cid:Pic1'/></body></html>";
            //var altView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            //altView.LinkedResources.Add(attachment);

            //mail.AlternateViews.Add(altView);

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
    }
}
