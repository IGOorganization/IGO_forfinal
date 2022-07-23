using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class OrderController : Controller
    {
        private readonly DemoIgoContext _IgoContext;  //注入
        private readonly IWebHostEnvironment _host;
        public OrderController(DemoIgoContext db, IWebHostEnvironment hostEnvironment)
        {
            _IgoContext = db;
            _host = hostEnvironment;
        }

        public IActionResult Order()
        {
            int userid = 0;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                Debug.WriteLine(userid);
                //return Json(userid);
            }
            //return Content(userid.ToString());  //抓到登入者的FCustomerId=47

            COrdersViewModel vModel = null;

            List<COrdersViewModel> v = new List<COrdersViewModel>();

            //var q = (from o in _context.TOrders
            //        where o.FCustomerId == 47 && o.FStatusId==1 || o.FStatusId == 3
            //         select o).ToList();
            var q = (from o in _IgoContext.TOrders
                     where o.FCustomerId == 47
                     select o).ToList();
            foreach (var r in q)
            {
                vModel = new COrdersViewModel
                {
                    OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderId,
                    OrderDate = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderDate,
                    StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatus.FStatusName,
                    TotalPrice = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FTotalPrice,
                    //PayType = _context.TOrders.Include(m => m.FPayType).FirstOrDefault(m => m.FOrderId == r.FOrderId).FPayType
                    OrderNum = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FOrderNum,
                    StatusId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == r.FOrderId).FStatusId
                };
                v.Add(vModel);
            }

            return View(v);
        }

        public IActionResult OrderDetailList(string Orderid)
        {
            string id = Orderid.Split("-")[1];
            int orderid = Convert.ToInt32(id);
            COrdersViewModel vModel = null;

            List<COrdersViewModel> v = new List<COrdersViewModel>();

            var q = (from o in _IgoContext.TOrderDetails
                     where o.FOrderId == orderid
                     select o).ToList();

            foreach (var r in q)
            {
                vModel = new COrdersViewModel
                {
                    OrderDetailsId = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FOrderDetailsId,
                    Price = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FPrice,
                    ProductName = _IgoContext.TOrderDetails.Include(m => m.FProduct).FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FProduct.FProductName,
                    Quantity = _IgoContext.TOrderDetails.FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FQuantity,
                    TicketName = _IgoContext.TOrderDetails.Include(m => m.FTicket).FirstOrDefault(m => m.FOrderDetailsId == r.FOrderDetailsId).FTicket.FTicketName,

                };
                v.Add(vModel);
            }
            return Json(v);
        }
        public IActionResult EditOrderStatus(string Orderid) //將訂單狀態改為取消申請中
        {
            string id = Orderid.Split("-")[1];
            int orderid = Convert.ToInt32(id);

            var order = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == orderid);
            if (order != null)
            {
                order.FStatusId = 5;
            }
            _IgoContext.SaveChanges();


            return RedirectToAction("Order");
        }
    }
}
