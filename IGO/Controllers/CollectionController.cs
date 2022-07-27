using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class CollectionController : Controller
    {
        private DemoIgoContext _dbIgo;
        public CollectionController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        DemoIgoContext db = new DemoIgoContext();

        public IActionResult List()
        {
            var datas = from t in _dbIgo.TProducts
                        select t;
            List<CProductViewModel> list = new List<CProductViewModel>();
            foreach (TProduct t in datas)
            {
                CProductViewModel p = new CProductViewModel(_dbIgo);
                p.product = t;
                list.Add(p);
            }
            return View(list);
        }



        public ActionResult _Fav(int? id)
        {

            TCollection item = new TCollection
            {

                FCustomerId = 1,
                FProductId = id,

                FCollectionDate = DateTime.Now.ToString(),

            };

            db.TCollections.Add(item);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult _UnFav(int? id)
        {

            TCollection item = db.TCollections.FirstOrDefault(t => t.FProductId == id);
            if (item != null)
            {
                db.TCollections.Remove(item);
                db.SaveChanges();

            };



            return RedirectToAction("List");
        }

        public IActionResult myFavList()
        {
            var datas = from t in _dbIgo.TCollections
                        select t;
            List<TCollection> list = new List<TCollection>();
            foreach (TCollection t in datas)
            {

                list.Add(t);
            }
            return View(list);
        }

        public IActionResult myFavGroup()
        {
            var datas = from t in _dbIgo.TCollectionGroups
                        select t;
            List<TCollectionGroup> list = new List<TCollectionGroup>();
            foreach (TCollectionGroup t in datas)
            {

                list.Add(t);
            }
            return View(list);
        }



        public IActionResult myFavGroupDetail()
        {
            var datas = from t in _dbIgo.TCollectionGroupDetails
                        select t;

            List<TCollectionGroupDetail> list = new List<TCollectionGroupDetail>();
            foreach (TCollectionGroupDetail t in datas)
            {

                list.Add(t);
            }
            return View(list);

        }


        public IActionResult CreateGroup()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateGroup(TCollectionGroup g)
        {

            db.TCollectionGroups.Add(g);
            db.SaveChanges();
            return RedirectToAction("myFavGroup");
        }

        public IActionResult CreateDetail()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateDetail(int id, int selectedid)
        {
            TCollectionGroupDetail item = new TCollectionGroupDetail
            {
                FCollectionGroupId = selectedid,
                FCollectionId =id,
            };


            db.TCollectionGroupDetails.Add(item);
            db.SaveChanges();
            return RedirectToAction("myFavGroup");
        }

        public IActionResult DisplayGroupDetail()
        {
            

            var Group=_dbIgo.TCollectionGroups.Select(x => new
            {
                FCollectionGroupId = x.FCollectionGroupId,
                FCollectionGroupName = x.FCollectionGroupName
            }).ToList();

            //var JData = JsonSerializer.Serialize(Group);

            return Json(Group);
        }









    }


  
 



 }

