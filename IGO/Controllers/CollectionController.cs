using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        private IWebHostEnvironment _enviroment;


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
            int userid = 0;
            string result = "";
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                result = "請先登入";
            }
            else
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);

                TCollection t = _dbIgo.TCollections.FirstOrDefault(n => n.FCustomerId == userid && n.FProductId == id);
                if (t == null)
                {
                    TCollection item = new TCollection
                    {
                        FCustomerId = userid,
                        FProductId = id,

                        FCollectionDate = DateTime.Now.ToString(),
                    };

                    db.TCollections.Add(item);
                    db.SaveChanges();

                    result = "已加入收藏";
                }
                else
                {
                    db.TCollections.Remove(t);
                    db.SaveChanges();
                    result = "取消收藏";
                }
            }
            return Json(result);
        }

        public ActionResult _UnFav(int? id)
        {
            
            TCollection item = db.TCollections.FirstOrDefault(t => t.FProductId == id);
            if (item == null)
            {
                return Json("null");
            }
            else
            {
                db.TCollections.Remove(item);
                db.SaveChanges();
            }
            return Json("success");
        }

        public IActionResult myFavList(int id)
        {
            var datas = from t in _dbIgo.TCollections
                        where t.FCustomerId == id
                        select t;
            if (datas.ToList().Count() == 0)
            {
                return Json("尚無收藏");
            }
            else
            {
                List<CCollectionViewModel> list = new List<CCollectionViewModel>();
                foreach (TCollection t in datas.ToList())
                {
                    CCollectionViewModel col = new CCollectionViewModel(_dbIgo);
                    CProductViewModel pvm = new CProductViewModel(_dbIgo);
                    TProduct tp = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == t.FProductId);

                    pvm.product = tp;
                    col.VMproduct = pvm;
                    col.collection = t;

                    list.Add(col);
                }
                //string result = System.Text.Json.JsonSerializer.Serialize(list);
                return Json(list);
            }

        }

        public IActionResult myFavGroup()
        {
            int userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == userid);
            return View(data);
        }

        public IActionResult getGroup()
        {
            int userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);

            IEnumerable<TCollectionGroup> list = _dbIgo.TCollectionGroups.Where(n => n.FCustomerId == userid);

            //string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(list);
        }


        public IActionResult getList()
        {
            int userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);

            IEnumerable<TCollection> list = _dbIgo.TCollections.Where(n => n.FCustomerId == userid);

            //string result = System.Text.Json.JsonSerializer.Serialize(list);
            return Json(list);
        }

        public IActionResult myFavGroupDetail(int id)
        {
            List<CCollectionViewModel> list = new List<CCollectionViewModel>();
            var datas = from t in _dbIgo.TCollectionGroupDetails
                        where t.FCollectionGroupId==id
                        select t;

            foreach (TCollectionGroupDetail t in datas)
            {
                TCollection tc = db.TCollections.FirstOrDefault(n => n.FCollectionId == t.FCollectionId);
                TProduct prod = db.TProducts.FirstOrDefault(p => p.FProductId == tc.FProductId);
                CProductViewModel pvm = new CProductViewModel(_dbIgo);
                CCollectionViewModel c = new CCollectionViewModel(db);
                pvm.product = prod;
                c.VMproduct = pvm;
                c.collection = tc;
                
                list.Add(c);


            }
            return Json(list);

        }


        public IActionResult CreateGroup()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateGroup(TCollectionGroup g)
        {
            
            string result = "";
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                result = "請先登入";
            }
            else {
                int userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
                g.FCustomerId = userid;
                db.TCollectionGroups.Add(g);
               db.SaveChanges();
               result = "收藏群組創立成功";
            }
            return RedirectToAction("myFavGroup"); 
        }

        //public IActionResult CreateDetail()
        //{
        //    return View();
        //}
        //[HttpPost]

        public IActionResult CreateDetail(int id, int selectedid)
        {
            IEnumerable<TCollectionGroupDetail> item = db.TCollectionGroupDetails.Where(t => t.FCollectionId== id);
            if (selectedid == 0)
            {
                return RedirectToAction("myFavGroup");
            }
            else if(item.Count()==0)
            {
                TCollectionGroupDetail t = new TCollectionGroupDetail();
                t.FCollectionId = id;
                t.FCollectionGroupId = selectedid;
                db.TCollectionGroupDetails.Add(t);
            }
            else
            {
                item.First().FCollectionGroupId = selectedid;
            }
            db.SaveChanges();
            return Json("加入收藏成功");
        }

        public IActionResult DisplayGroupDetail()
        {


            var Group = _dbIgo.TCollectionGroups.Select(x => new
            {
                FCollectionGroupId = x.FCollectionGroupId,
                FCollectionGroupName = x.FCollectionGroupName
            }).ToList();

            //var JData = JsonSerializer.Serialize(Group);

            return Json(Group);
        }

        //public IActionResult addToGroup(int group)
        //{

        //}



    }







}

