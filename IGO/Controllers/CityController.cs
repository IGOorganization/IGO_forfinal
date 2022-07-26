using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CityController : Controller
    {
        private DemoIgoContext _IgoContext;  //注入
        private readonly IWebHostEnvironment _host;
        public CityController(DemoIgoContext db, IWebHostEnvironment hostEnvironment)
        {
            _IgoContext = db;
            _host = hostEnvironment;
        }
        public IActionResult PopularCity(CCityViewModel vModel)  //台北熱賣排行
        {
            var q = (from od in _IgoContext.TOrderDetails.Include(od => od.FProduct)
                     where od.FProduct.FCityId == 1
                     group od by od.FProductId into g
                     orderby g.Count() descending
                     select g.Key).Take(8).ToList();

            List<CCityViewModel> v = new List<CCityViewModel>();

            foreach (int r in q)
            {
                vModel = new CCityViewModel()
                {
                    ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FProductName,
                    PhotoPath = _IgoContext.TProductsPhotos.FirstOrDefault(m => m.FProductId == r && m.FPhotoSiteId == 2).FPhotoPath,
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r).FTicket.FTicketName,
                    ProductId = r
                };
                v.Add(vModel);
            }
            return View(v);
        }
        public IActionResult PriceHighToLow()  //台北價格高到低排序
        {
            var q = (from p in _IgoContext.TProducts.Include(m => m.TTicketAndProducts)
                     where p.FCityId == 1
                     select p).ToList().OrderByDescending(p => p.TTicketAndProducts.FirstOrDefault().FPrice).Take(8);

            CCityViewModel vModel = null;
            List<CCityViewModel> v = new List<CCityViewModel>();
            foreach (var r in q)
            {
                //Debug.WriteLine(r.TTicketAndProducts.FirstOrDefault().FPrice);
                int productID = r.FProductId;

                vModel = new CCityViewModel()
                {
                    ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == productID).FProductName,
                    CompanyName = _IgoContext.TProducts.Include(m => m.FSupplier).FirstOrDefault(m => m.FProductId == productID).FSupplier.FCompanyName,
                    PhotoPath = _IgoContext.TProductsPhotos.FirstOrDefault(m => m.FProductId == productID && m.FPhotoSiteId == 2).FPhotoPath,
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == productID).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == productID).FTicket.FTicketName,
                    ProductId = productID

                };
                v.Add(vModel);

            }
            //return View(v);
            return Json(v);
        }

        public IActionResult PriceLowToHigh()  //台北價格低到高排序
        {
            var q = (from p in _IgoContext.TProducts.Include(m => m.TTicketAndProducts)
                     where p.FCityId == 1
                     select p).ToList().OrderBy(p => p.TTicketAndProducts.FirstOrDefault().FPrice).Take(8);

            CCityViewModel vModel = null;
            List<CCityViewModel> v = new List<CCityViewModel>();
            foreach (var r in q)
            {
                Debug.WriteLine(r.TTicketAndProducts.FirstOrDefault().FPrice);
                int productID = r.FProductId;

                vModel = new CCityViewModel()
                {
                    ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == productID).FProductName,
                    CompanyName = _IgoContext.TProducts.Include(m => m.FSupplier).FirstOrDefault(m => m.FProductId == productID).FSupplier.FCompanyName,
                    PhotoPath = _IgoContext.TProductsPhotos.FirstOrDefault(m => m.FProductId == productID && m.FPhotoSiteId == 2).FPhotoPath,
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == productID).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == productID).FTicket.FTicketName,
                    ProductId = productID

                };
                v.Add(vModel);

            }
            return Json(v);
        }
        public IActionResult HotSaleCity(CCityViewModel vModel)  //台北熱賣排行
        {
            var q = (from od in _IgoContext.TOrderDetails.Include(od => od.FProduct)
                     where od.FProduct.FCityId == 1
                     group od by od.FProductId into g
                     orderby g.Count() descending
                     select g.Key).Take(8).ToList();

            List<CCityViewModel> v = new List<CCityViewModel>();

            foreach (int r in q)
            {
                vModel = new CCityViewModel()
                {
                    ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FProductName,
                    PhotoPath = _IgoContext.TProductsPhotos.FirstOrDefault(m => m.FProductId == r && m.FPhotoSiteId == 2).FPhotoPath,
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r).FTicket.FTicketName,
                    ProductId = r
                };
                v.Add(vModel);
            }
            return Json(v);
        }
    }
}
