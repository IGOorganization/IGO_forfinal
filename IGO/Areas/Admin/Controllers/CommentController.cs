using Comment.ViewModels;
using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CommentController : Controller
    {
        private DemoIgoContext _dbIgo;
        public CommentController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(CKeywordViewModel vModel)
        {
            List<CFeedbackManagementViewModel> lists = new List<CFeedbackManagementViewModel>();
            IEnumerable<TFeedbackManagement> datas = null;

            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in _dbIgo.TFeedbackManagements
                        select t;
            }
            else

            {
                datas = _dbIgo.TFeedbackManagements.Where(t => t.FProduct.FProductName.Contains(vModel.txtKeyword));
            }
            foreach (var data in datas)
            {
                CFeedbackManagementViewModel cFeedbackManagementViewModel = new CFeedbackManagementViewModel(_dbIgo);
                cFeedbackManagementViewModel.feedbackManagement = data;
                lists.Add(cFeedbackManagementViewModel);
            }

            return View(lists);
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
        public ActionResult Edit(CProductViewModel p)
        {

            DemoIgoContext db = new DemoIgoContext();
            TFeedbackManagement prod = db.TFeedbackManagements.FirstOrDefault(t => t.FFeedbackId == p.FeedbackId);
            if (prod != null)
            {
                //if (p.photo != null)
                //{
                //    string pName = Guid.NewGuid().ToString() + ".jpg";
                //    p.photo.CopyTo(new FileStream(
                //        _enviroment.WebRootPath + "/images/" + pName, FileMode.Create));
                //    //prod.ImagePath = pName;
                //}
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
            return RedirectToAction("List");
        }

    }
}
