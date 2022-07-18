using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class LiveController : Controller
    {
        DemoIgoContext _dbIgo;
        public LiveController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult List()
        {
            IEnumerable<TSupplier> supliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1);
            return View(supliers);
        }
        public IActionResult getRoomTypeBySupplier(int supplierid)
        {
            IEnumerable<TProduct> products = _dbIgo.TProducts.Where(n => n.FSupplierId == supplierid && n.FSubCategoryId == 1);
            if (products != null)
            {
                List<CRoomtypeViewModel> list = new List<CRoomtypeViewModel>();
                foreach (TProduct p in products.ToList())
                {
                    IEnumerable<TTicketAndProduct> items = _dbIgo.TTicketAndProducts.Where(n => n.FProductId == p.FProductId);
                    foreach (TTicketAndProduct tap in items.ToList())
                    {
                        CRoomtypeViewModel cp = new CRoomtypeViewModel(_dbIgo);
                        cp.ProductId = tap.FProductId;
                        cp.ticketId = tap.FTicketId;
                        list.Add(cp);
                    }
                }
                return Json(list);
            }
            return Json(null);
        }
        public IActionResult CreateRoom()
        {

            return Json(null);
        }
    }
}
