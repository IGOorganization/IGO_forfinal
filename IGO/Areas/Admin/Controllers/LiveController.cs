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
        public IActionResult CreateRoom(CAdminLiveViewModel c)
        {
            TSupplier supplier = _dbIgo.TSuppliers.FirstOrDefault(n => n.FSupplierId == c.supplier);
            TProduct t = new TProduct()
            {
                FProductName = c.fProductName,
                FCityId = supplier.FCityId,
                FAddress = supplier.FAddress,
                FSupplierId = c.supplier,
                FQuantity = c.Quentity,
                FStartTime = DateTime.Now.ToString("yyyy-MM-dd"),
                FEndTime = DateTime.Now.AddYears(100).ToString("yyyy-MM-dd"),
                FSubCategoryId = 1,
                FIntroduction = c.fIntroduction
            };
            _dbIgo.TProducts.Add(t);
            _dbIgo.SaveChanges();

            TTicketAndProduct ticket = new TTicketAndProduct()
            {
                FProductId = _dbIgo.TProducts.Where(n => n.FSubCategoryId == 1).OrderBy(n => n.FProductId).Last().FProductId,
                FTicketId = c.tickettype,
                FPrice = c.price
            };
            _dbIgo.TTicketAndProducts.Add(ticket);
            _dbIgo.SaveChanges();

            return RedirectToAction("getRoomByID", new { id = c.fTicketAndProductId });
        }
        public IActionResult getHotelList()
        {
            IEnumerable<TSupplier> supliers = _dbIgo.TSuppliers.Where(n => n.FSubCategoryId == 1);
            return Json(supliers);
        }
        public IActionResult Delete(int id)
        {
            TTicketAndProduct t = _dbIgo.TTicketAndProducts.Find(id);
            int Supplierid = (int)_dbIgo.TTicketAndProducts.Include(n => n.FProduct).FirstOrDefault(n => n.FTicketAndProductId == id).FProduct.FSupplierId;
            _dbIgo.TTicketAndProducts.Remove(t);
            _dbIgo.SaveChanges();
            return RedirectToAction("getRoomTypeBySupplier", new { supplierid = Supplierid });
        }
        public IActionResult getRoomByID(int id)
        {
            List<CRoomtypeViewModel> list = new List<CRoomtypeViewModel>();
            TTicketAndProduct item = _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FTicketAndProductId == id);

            CRoomtypeViewModel cp = new CRoomtypeViewModel(_dbIgo);
            cp.ProductId = item.FProductId;
            cp.ticketId = item.FTicketId;
            list.Add(cp);

            return Json(list);
        }
        public IActionResult Edit(CAdminLiveViewModel c)
        {
            TTicketAndProduct tapbase = _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FProductId == c.fProductId && n.FTicketId == c.tickettype);
            TTicketAndProduct tap = _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FTicketAndProductId == c.fTicketAndProductId);
            if (tapbase.FTicketAndProductId != tap.FTicketAndProductId)
            {
                return Json(null);
            }
            else
            {
                TProduct t = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == c.fProductId);
                t.FProductName = c.fProductName;
                t.FSupplierId = c.supplier;
                t.FQuantity = c.Quentity;
                t.FIntroduction = c.fIntroduction;

                tap.FTicketId = c.tickettype;
                tap.FPrice = c.price;
                _dbIgo.SaveChanges();

                return RedirectToAction("getRoomByID", new { id = c.fTicketAndProductId });
            }
        }
    }
}
