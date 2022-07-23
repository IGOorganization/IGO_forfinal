﻿using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DemoIgoContext _IgoContext;

        private readonly IWebHostEnvironment _host;

        public HomeController(ILogger<HomeController> logger, DemoIgoContext db, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _IgoContext = db;
            _host = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Home(CHomeViewModel vModel)
        {
            var q = (from o in _IgoContext.TOrderDetails
                     group o by o.FProductId into g
                     orderby g.Count() descending
                     select g.Key.Value  // key=o.FProductId
                    ).Take(3).ToList();

            //CHomeViewModel vModel = null;

            List<CHomeViewModel> v = new List<CHomeViewModel>();
            //_context.TCities.ToList();
            //_context.TProductsPhotos.ToList();

            foreach (int r in q)
            {
                vModel = new CHomeViewModel()
                {
                    tProduct = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r),
                    tCity = _IgoContext.TProducts.Include(m => m.FCity).FirstOrDefault(m => m.FProductId == r).FCity,
                    ProductPhotoId = (int)_IgoContext.TProductsPhotos.Where(m => m.FProductId == r).FirstOrDefault(a => a.FPhotoSiteId == 1).FProductPhotoId,
                    PhotoPath = _IgoContext.TProductsPhotos.FirstOrDefault(m => m.FProductId == r).FPhotoPath,
                    RankingCount = (int)(from f in _IgoContext.TFeedbackManagements
                                         where f.FProductId == r
                                         select f).Average(m => m.FRanking),
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r).FTicket.FTicketName,
                    //EndTime = _context.TProducts.FirstOrDefault(m => m.FProductId == r).FEndTime,
                    StorageQuantity = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FQuantity,
                    //OrderQuantity寫法1:
                    OrderQuantity = (from o in _IgoContext.TOrderDetails
                                     group o by o.FProductId into g
                                     where g.Key == r
                                     select new { key = g.Key, count = g.Sum(od => od.FQuantity) }).FirstOrDefault().count
                    //OrderQuantity寫法2:
                    //OrderQuantity = (from o in _context.TOrderDetails
                    //                 where o.FProductId == r
                    //                 select o).Sum(o => o.FQuantity)
                };
                //Debug.WriteLine(vModel.productRanking)
                v.Add(vModel);
            }
            return View(v);

        }
        public IActionResult SubCategory() //票卷種類
        {
            var q = from c in _IgoContext.TSubCategories
                    select c;

            return Json(q);
        }
        public IActionResult top3City()
        {
            var top3HotCity = (from od in _IgoContext.TOrderDetails.Include(od => od.FProduct).ThenInclude(p => p.FCity)
                               group od by new { od.FProduct.FCityId } into g
                               select new { key = g.Key, count = g.Count() }).OrderByDescending(g => g.count).Take(3).ToList();

            //return View(top3HotCity);
            CHomeViewModel vModel = null;

            List<CHomeViewModel> v = new List<CHomeViewModel>();

            foreach (var r in top3HotCity)
            {
                vModel = new CHomeViewModel()
                {
                    CityName = _IgoContext.TCities.FirstOrDefault(m => m.FCityId == r.key.FCityId).FCityName,
                    CityPhotoPath = _IgoContext.TCities.FirstOrDefault(m => m.FCityId == r.key.FCityId).FCityPhotoPath
                };
                v.Add(vModel);
            }

            return Json(v);
        }

        public IActionResult TopFeedback()
        {
            var topFeedbackId = (from f in _IgoContext.TFeedbackManagements
                                 where f.FFeedbackContent.Length > 30
                                 orderby f.FRanking descending
                                 select f.FFeedbackId).Take(3).ToList();

            CHomeViewModel vModel = null;

            List<CHomeViewModel> v = new List<CHomeViewModel>();
            _IgoContext.TProducts.ToList();
            _IgoContext.TCustomers.ToList();
            foreach (int f in topFeedbackId)
            {
                vModel = new CHomeViewModel()
                {
                    FeedbackContent = _IgoContext.TFeedbackManagements.FirstOrDefault(m => m.FFeedbackId == f).FFeedbackContent,
                    Ranking = _IgoContext.TFeedbackManagements.FirstOrDefault(m => m.FFeedbackId == f).FRanking,
                    LastName = _IgoContext.TFeedbackManagements.Include(m => m.FCustomer).FirstOrDefault(m => m.FFeedbackId == f).FCustomer.FLastName,
                    ProductName = _IgoContext.TFeedbackManagements.Include(m => m.FProduct).FirstOrDefault(m => m.FFeedbackId == f).FProduct.FProductName
                };
                v.Add(vModel);
            }

            return Json(v);

        }
        public IActionResult AboutIGO()
        {
            CHomeViewModel vModel = null;
            vModel = new CHomeViewModel()
            {
                customerCount = (from c in _IgoContext.TCustomers
                                 select c).Count()
            };
            return Json(vModel);
        }
        public IActionResult layoutCategory()
        {
            var id = (from c in _IgoContext.TCategories
                      select c.FCategoryId).ToList();

            CHomeViewModel vModel = null;
            List<CHomeViewModel> v = new List<CHomeViewModel>();
            foreach (int i in id)
            {
                vModel = new CHomeViewModel()
                {
                    CategoryName = _IgoContext.TCategories.FirstOrDefault(m => m.FCategoryId == i).FCategoryName,
                    CategoryId = _IgoContext.TCategories.FirstOrDefault(m => m.FCategoryId == i).FCategoryId

                };
                v.Add(vModel);
            }
            return Json(v);
        }
        public IActionResult layoutSubCategory(int idd)
        {
            var q = (from c in _IgoContext.TSubCategories
                     select c.FSubCategoryId).ToList();

            CHomeViewModel vModel = null;

            vModel = new CHomeViewModel()
            {
                SubCategoryNameList = _IgoContext.TSubCategories.Where(m => m.FCategoryId == idd).Select(m => m.FSubCategoryName).ToList()
            };

            return Json(vModel);
        }

        //鈞傑
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            TCustomer cust = _IgoContext.TCustomers.FirstOrDefault(n => n.FPhone == vModel.txtAccount);
            if (cust != null)
            {
                if (cust.FPassword.Equals(vModel.txtPassword))
                {
                    HttpContext.Session.SetInt32(CDictionary.SK_LOGINED_USER, cust.FCustomerId);

                    return RedirectToAction("Home", "Home");
                }
            }
            return View();
        }
        public IActionResult checkUser()
        {
            int userid = 0;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                return Json(userid);
            }
            return Json("尚未登入");
        }
    }
}
