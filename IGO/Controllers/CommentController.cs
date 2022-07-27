using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using IGO.Models;
using Comment.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IGO.ViewModels;
using prjMvcCoreDemo.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Comment.Controllers
{
    public class CommentController : Controller
    {

       

        private IWebHostEnvironment _enviroment;
        private readonly DemoIgoContext _db;
        int userid = 0;
        public CommentController(IWebHostEnvironment p, DemoIgoContext db)
        {
            _enviroment = p;
            _db = db;
        }

        //public IActionResult List(CKeywordViewModel vModel)
        //{
        //    int UserID = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
        //    IEnumerable<TFeedbackManagement> datas = null;



        //    if (string.IsNullOrEmpty(vModel.txtKeyword))
        //    {
        //        datas = from t in _db.TFeedbackManagements
        //                select t;
        //    }
        //    else

        //    {
        //        datas = _db.TFeedbackManagements.Where(t => t.FProduct.FProductName.Contains(vModel.txtKeyword));
        //    }


        //    return View(datas.ToList());
        //}

        public IActionResult CommentList ()
        {
            List<CFeedbackManagementViewModel> lists = new List<CFeedbackManagementViewModel>();
            userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
            IEnumerable<TFeedbackManagement> datas = _db.TFeedbackManagements.Where(t => t.FCustomerId == userid);
            foreach (var data in datas) {
                CFeedbackManagementViewModel cFeedbackManagementViewModel = new CFeedbackManagementViewModel(_db);
                cFeedbackManagementViewModel.feedbackManagement = data;
                lists.Add(cFeedbackManagementViewModel);
            }
            return View(lists);
        }
    
    
   

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TFeedbackManagement p)
        {
            int UserID = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
            ViewBag.UserName = (_db.TCustomers.FirstOrDefault(c => c.FCustomerId == UserID)).FCustomerId;

            _db.TFeedbackManagements.Add(p);
            //FeedbackManagement prod = db.FeedbackManagements.FirstOrDefault(t => t.FeedbackId == p.FeedbackId);
            

            _db.SaveChanges();
            return RedirectToAction("CommentList");
        }
        public ActionResult Edit(int? id)
        {
            DemoIgoContext db = new DemoIgoContext();
            TFeedbackManagement prod = db.TFeedbackManagements.FirstOrDefault(t => t.FFeedbackId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public ActionResult Edit(CFeedbackProductViewModel p)
        {

            DemoIgoContext db = new DemoIgoContext();
            TFeedbackManagement prod = db.TFeedbackManagements.FirstOrDefault(t => t.FFeedbackId == p.FeedbackId);
            if (prod != null)
            {
                if (p.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    p.photo.CopyTo(new FileStream(
                        _enviroment.WebRootPath + "/images/" + pName, FileMode.Create));
                    //prod.ImagePath = pName;
                }
                //prod.CustomerId = p.CustomerId;
                prod.FFeedbackContent = p.FeedbackContent;
                prod.FRanking = p.Ranking;
                //prod.ProductsId = p.ProductsId;
                //prod.FeedbackDate = p.FeedbackDate;
            }
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            DemoIgoContext db = new DemoIgoContext();
            TFeedbackManagement prod = db.TFeedbackManagements.FirstOrDefault(t => t.FFeedbackId == id);
            if (prod != null)
            {
                db.TFeedbackManagements.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("CommentList");
        }
    }
}
