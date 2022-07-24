using IGO.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class DiscountController : Controller
    {
        DemoIgoContext _dbIgo;
        private IWebHostEnvironment _enviroment;
        public DiscountController(DemoIgoContext db, IWebHostEnvironment p)
        {
            _dbIgo = db;
            _enviroment = p;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult getAll()
        {
            var q = _dbIgo.TDiscounts.ToList();
            return Json(q);
        }
        public IActionResult getDiscountById(int id)
        {
            return Json(_dbIgo.TDiscounts.FirstOrDefault(n => n.FDiscountId == id));
        }
        public IActionResult Create(TDiscount d,IFormFile Photo)
        {
            if (Photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Photo.CopyTo(new FileStream(_enviroment.WebRootPath + "/img/" + pName, FileMode.Create));
                d.FImagePath = pName;
            }

            _dbIgo.TDiscounts.Add(d);
            _dbIgo.SaveChanges();
            List<TDiscount> list = new List<TDiscount>();
            list.Add(_dbIgo.TDiscounts.OrderBy(n => n.FDiscountId).Last());
            return Json(list);
        }
        public IActionResult Edit(TDiscount d,IFormFile Photo)
        {
            TDiscount dis = _dbIgo.TDiscounts.FirstOrDefault(n => n.FDiscountId == d.FDiscountId);

            if (Photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Photo.CopyTo(new FileStream(_enviroment.WebRootPath + "/img/" + pName, FileMode.Create));
                dis.FImagePath = pName;
            }

            dis.FDiscountName = d.FDiscountName;
            dis.FDiscount = d.FDiscount;
            dis.FDeadTime = d.FDeadTime;
            dis.FTimeOut = d.FTimeOut;
            _dbIgo.SaveChanges();
            
            return Json(_dbIgo.TDiscounts.Where(n=>n.FDiscountId==d.FDiscountId));
        }
        public IActionResult Delete(int id)
        {
            TDiscount dis = _dbIgo.TDiscounts.FirstOrDefault(n=>n.FDiscountId==id);
            _dbIgo.TDiscounts.Remove(dis);
            _dbIgo.SaveChanges();
            return Json(_dbIgo.TDiscounts.ToList());
        }
    }
}
