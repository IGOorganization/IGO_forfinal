using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class DiscountController : Controller
    {
        DemoIgoContext _dbIgo;
        public DiscountController(DemoIgoContext db)
        {
            _dbIgo = db;
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
        public IActionResult Create(TDiscount d)
        {
            _dbIgo.TDiscounts.Add(d);
            _dbIgo.SaveChanges();
            return Json(_dbIgo.TDiscounts.ToList());
        }
        public IActionResult Edit(TDiscount d)
        {
            TDiscount dis = _dbIgo.TDiscounts.FirstOrDefault(n => n.FDiscountId == d.FDiscountId);
            dis.FDiscountName = d.FDiscountName;
            dis.FDiscount = d.FDiscount;
            dis.FDeadTime = d.FDeadTime;
            dis.FTimeOut = d.FTimeOut;
            _dbIgo.SaveChanges();
            
            return Json(_dbIgo.TDiscounts.Where(n=>n.FDiscountId==d.FDiscountId));
        }
    }
}
