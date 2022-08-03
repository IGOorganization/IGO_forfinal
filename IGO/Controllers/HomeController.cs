using IGO.Models;
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
        //首頁:熱門商品排行
        public IActionResult Home(CHomeViewModel vModel)
        {
            var q = (from o in _IgoContext.TOrderDetails
                     group o by o.FProductId into g
                     orderby g.Count() descending
                     select g.Key.Value  // key=o.FProductId
                    ).Take(3).ToList();

            List<CHomeViewModel> v = new List<CHomeViewModel>();


            foreach (int r in q)
            {
                //7/31宜潔新增無評論防呆
                var lenRanking = (from f in _IgoContext.TFeedbackManagements
                                  where f.FProductId == r
                                  select f.FRanking).ToList();
                //7/31宜潔新增產品無照片防呆
                var len = (from f in _IgoContext.TProductsPhotos
                           where f.FProductId == r && f.FPhotoSiteId == 1
                           select f.FPhotoPath).ToList();

                //7/31宜潔新增產品結束日期小於今日防呆
                DateTime today = DateTime.Now.Date;    /*今日日期 */
                var End = from f in _IgoContext.TProducts     
                          where f.FProductId == r
                           select f.FEndTime;
                DateTime day = Convert.ToDateTime(End.First());   /*產品結束日期 */
                TimeSpan time = (Convert.ToDateTime(End.First())).Date - today;
                
                if (lenRanking.Count > 0 && len.Count > 0 && time.Days >= 1)
                { 
                vModel = new CHomeViewModel()
                {
                    tProduct = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r),
                    tCity = _IgoContext.TProducts.Include(m => m.FCity).FirstOrDefault(m => m.FProductId == r).FCity,
                    //7/30宜潔修改
                    PhotoPath = _IgoContext.TProductsPhotos.Where(m => m.FProductId == r).FirstOrDefault(a => a.FPhotoSiteId == 1).FPhotoPath,
                    RankingCount = (int)(from f in _IgoContext.TFeedbackManagements
                                         where f.FProductId == r
                                         select f).Average(m => m.FRanking),
                    Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r).FPrice,
                    TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r).FTicket.FTicketName,
                    EndTime = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FEndTime,
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
                else if(lenRanking.Count > 0 && time.Days >= 1)
                {
                    vModel = new CHomeViewModel()
                    {
                        tProduct = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r),
                        tCity = _IgoContext.TProducts.Include(m => m.FCity).FirstOrDefault(m => m.FProductId == r).FCity,
                        //7/30宜潔修改
                        PhotoPath = "aaf2c4d4-e523-4563-8fa6-094e19d641e2.jpg",
                        RankingCount = (int)(from f in _IgoContext.TFeedbackManagements
                                             where f.FProductId == r
                                             select f).Average(m => m.FRanking),
                        Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r).FPrice,
                        TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r).FTicket.FTicketName,
                        EndTime = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FEndTime,
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
            }
            return View(v);

        }
        //首頁:點閱次數排行
        public IActionResult topViewRecord()
        {
            var q = (from p in _IgoContext.TProducts
                     orderby p.FViewRecord descending
                     select p).Take(3).ToList();

            CHomeViewModel vModel = null;

            List<CHomeViewModel> v = new List<CHomeViewModel>();
            foreach (var r in q)
            {
                //7/31宜潔新增無評論防呆
                var lenRanking = (from f in _IgoContext.TFeedbackManagements
                              where f.FProductId == r.FProductId
                              select f.FRanking).ToList();
                //7/31宜潔新增產品無照片防呆
                var lenPhoto = (from f in _IgoContext.TProductsPhotos
                           where f.FProductId == r.FProductId && f.FPhotoSiteId == 1
                           select f.FPhotoPath).ToList();

                ////7/31宜潔新增產品結束日期小於今日防呆
                //DateTime today = DateTime.Now.Date;    /*今日日期 */
                //var End = from f in _IgoContext.TProducts
                //          where f.FProductId == r.FProductId
                //          select f.FEndTime;
                //DateTime day = Convert.ToDateTime(End.First());   /*產品結束日期 */
                //TimeSpan time = (Convert.ToDateTime(End.First())).Date - today;

                if (lenRanking.Count > 0 && lenPhoto.Count > 0)
                {
                    vModel = new CHomeViewModel()
                    {
                        tProduct = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId),
                        tCity = _IgoContext.TProducts.Include(m => m.FCity).FirstOrDefault(m => m.FProductId == r.FProductId).FCity,                        
                        PhotoPath = _IgoContext.TProductsPhotos.Where(m => m.FProductId == r.FProductId).FirstOrDefault(a => a.FPhotoSiteId == 1).FPhotoPath,
                        ViewRecord = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FViewRecord,
                        RankingCount = (int)(from f in _IgoContext.TFeedbackManagements
                                             where f.FProductId == r.FProductId
                                             select f).Average(m => m.FRanking),
                        Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FPrice,
                        TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r.FProductId).FTicket.FTicketName,
                        //EndTime = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FEndTime,
                        StorageQuantity = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FQuantity,
                        //OrderQuantity = (from o in _IgoContext.TOrderDetails
                        //                 group o by o.FProductId into g
                        //                 where g.Key == r.FProductId
                        //                 select new { key = g.Key, count = g.Sum(od => od.FQuantity) }).FirstOrDefault().count
                    };
                    v.Add(vModel);
                }
                else if (lenRanking.Count > 0) 
                {
                    vModel = new CHomeViewModel()
                    {
                        tProduct = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId),
                        tCity = _IgoContext.TProducts.Include(m => m.FCity).FirstOrDefault(m => m.FProductId == r.FProductId).FCity,                      
                        PhotoPath = "aaf2c4d4-e523-4563-8fa6-094e19d641e2.jpg",
                        ViewRecord = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FViewRecord,
                        RankingCount = (int)(from f in _IgoContext.TFeedbackManagements
                                             where f.FProductId == r.FProductId
                                             select f).Average(m => m.FRanking),
                        Price = _IgoContext.TTicketAndProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FPrice,
                        TicketName = _IgoContext.TTicketAndProducts.Include(m => m.FTicket).FirstOrDefault(m => m.FProductId == r.FProductId).FTicket.FTicketName,
                        //EndTime = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FEndTime,
                        StorageQuantity = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r.FProductId).FQuantity,
                        //OrderQuantity = (from o in _IgoContext.TOrderDetails
                        //                 group o by o.FProductId into g
                        //                 where g.Key == r.FProductId
                        //                 select new { key = g.Key, count = g.Sum(od => od.FQuantity) }).FirstOrDefault().count
                    };
                    v.Add(vModel);
                }             
            }

            return Json(v);
        }

        //點閱後瀏覽次數增加 7/27新增
        public IActionResult AddViewRecord(int Productid)
        {
            var product = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == Productid);
            int count = (int)_IgoContext.TProducts.FirstOrDefault(m => m.FProductId == Productid).FViewRecord;
            int viewcounts = count + 1;
            if (product != null)
            {
                product.FViewRecord = viewcounts;

            }
            _IgoContext.SaveChanges();

            return View();
        }



        //票卷種類
        public IActionResult SubCategory() 
        {
            var q = from c in _IgoContext.TSubCategories
                    select c;

            return Json(q);
        }

        //首頁:熱門城市排行
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
        //首頁:評論
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
        //首頁:關於IGO
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
        //Layout的商品主分類
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
        public IActionResult layoutCategoryPhoto(int idd)
        {
            //var id = (from c in _IgoContext.TCategories
            //          select c.FCategoryId).ToList();

            CHomeViewModel vModel = null;
            //List<CHomeViewModel> v = new List<CHomeViewModel>();

                vModel = new CHomeViewModel()
                {
                   
                    CategoryPhotoPath = _IgoContext.TCategories.FirstOrDefault(m => m.FCategoryId == idd).FCategoryPhotoPath

                };
            
            return Json(vModel);

        }
        //Layout的商品次分類
        public IActionResult layoutSubCategory(int idd)
        {
            var q = _IgoContext.TSubCategories.Where(m => m.FCategoryId == idd).ToList();

            CHomeViewModel vModel = null;
            List<CHomeViewModel> v = new List<CHomeViewModel>();
            foreach (var r in q) 
            { 
            vModel = new CHomeViewModel()
            {
                CategoryName = _IgoContext.TSubCategories.Include(m=>m.FCategory).FirstOrDefault(m=>m.FSubCategoryId==r.FSubCategoryId).FCategory.FCategoryName,
                SubCategoryName = _IgoContext.TSubCategories.FirstOrDefault(m=>m.FSubCategoryId==r.FSubCategoryId).FSubCategoryName,
                SubCategoryPath = _IgoContext.TSubCategories.FirstOrDefault(m => m.FSubCategoryId == r.FSubCategoryId).FSubCategoryPath
            };
                v.Add(vModel);
            }
            return Json(v);
        }
        //Layout的瀏覽紀錄:待做
        public IActionResult layoutViewContent(int prodId, string path)
        {           
            CHomeViewModel vModel = null;

            vModel = new CHomeViewModel()
             {

                ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == prodId).FProductName
                    
            };            
            return Json(vModel);
        }
        public IActionResult News(CHomeViewModel vModel) //7/30宜潔新增廣告輪播的最新消息
        {
            var q = (from p in _IgoContext.TProducts
                    orderby p.FEndTime
                    select p.FProductId).Take(1).ToList();
            List<CHomeViewModel> v = new List<CHomeViewModel>();
            foreach (var r in q) 
            {
                vModel = new CHomeViewModel()
                {
                ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == r).FProductName,
                    ProductId = r,
                    ProductPhotoId = (int)_IgoContext.TProductsPhotos.Where(m => m.FProductId == r).FirstOrDefault(a => a.FPhotoSiteId == 1).FProductPhotoId,
                PhotoPath = _IgoContext.TProductsPhotos.Where(m => m.FProductId == r).FirstOrDefault(a => a.FPhotoSiteId == 1).FPhotoPath,
                };
                v.Add(vModel);
            }
            
            return Json(v);
        }
        public IActionResult SpacialCombination(CHomeViewModel vModel) //7/30宜潔新增廣告輪播的優惠組合
        {       
            List<CHomeViewModel> v = new List<CHomeViewModel>();

                vModel = new CHomeViewModel()
                {
                    ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == 517).FProductName,
                    ProductPhotoId = (int)_IgoContext.TProductsPhotos.Where(m => m.FProductId == 517).FirstOrDefault(a => a.FPhotoSiteId == 1).FProductPhotoId,
                    PhotoPath = _IgoContext.TProductsPhotos.Where(m => m.FProductId == 517).FirstOrDefault(a => a.FPhotoSiteId == 1).FPhotoPath,
                };
                v.Add(vModel);

            return Json(v);
        }

        public IActionResult Test(CHomeViewModel vModel) //7/30宜潔新增測試用
        {
            vModel = new CHomeViewModel()
            {
                ProductName = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == 267).FProductName,

                EndTime = _IgoContext.TProducts.FirstOrDefault(m => m.FProductId == 267).FEndTime

            };
            return View(vModel);

        }



    }
}
