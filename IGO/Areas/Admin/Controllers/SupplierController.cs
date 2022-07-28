using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    [Area(areaName: "Admin")]
    public class SupplierController : Controller
    {
        
            private IWebHostEnvironment _enviroment;
            public SupplierController(IWebHostEnvironment p)
            {
                _enviroment = p;
            }



            public IActionResult Index()
            {
                return View();
            }

            DemoIgoContext db = new DemoIgoContext();

            public IActionResult List(CKeyWord vModel)
            {

                IEnumerable<TSupplier> datas = null;
                if (string.IsNullOrEmpty(vModel.txtKeyword))
                {
                    datas = from t in db.TSuppliers
                            select t;
                }
                else
                {
                    datas = db.TSuppliers.Where(t => t.FCompanyName.Contains(vModel.txtKeyword));
                }
                return View(datas);
            }



            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Create(TSupplier p)
            {

                db.TSuppliers.Add(p);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            public ActionResult Edit(int? id)
            {

                TSupplier supplier = db.TSuppliers.FirstOrDefault(t => t.FSupplierId == id);
                if (supplier == null)
                    return RedirectToAction("List");
                return View(supplier);
            }
            [HttpPost]
            public ActionResult Edit(CSupplierViewModel p)
            {


                TSupplier supplier = db.TSuppliers.FirstOrDefault(t => t.FSupplierId == p.FSupplierId);
                if (supplier != null)
                {

                    supplier.FCompanyName = p.FCompanyName;
                    supplier.FPhone = p.FPhone;
                    supplier.FAddress = p.FAddress;

                }
                db.SaveChanges();
                return RedirectToAction("List");
            }
            public IActionResult Delete(int? id)
            {

                TSupplier supplier = db.TSuppliers.FirstOrDefault(t => t.FSupplierId == id);
                if (supplier != null)
                {
                    db.TSuppliers.Remove(supplier);
                    db.SaveChanges();
                }
                return RedirectToAction("List");
            }

        }
    }


