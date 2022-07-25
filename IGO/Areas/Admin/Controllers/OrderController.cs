using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //var q = (from o in _IgoContext.TOrders
            //         where o.FOrderId == orderID
            //         select o).ToList();

            COrdersViewModel vModel = null;


            vModel = new COrdersViewModel
            {
                OrderId = _IgoContext.TOrders.FirstOrDefault(m => m.FOrderId == orderID).FOrderId,
                StatusName = _IgoContext.TOrders.Include(m => m.FStatus).FirstOrDefault(m => m.FOrderId == orderID).FStatus.FStatusName
            };
            //return Content(orderID.ToString());
            return Json(vModel);

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
    }
}
