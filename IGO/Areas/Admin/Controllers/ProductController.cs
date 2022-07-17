using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class ProductController : Controller
    {
        DemoIgoContext _dbIgo;
        public ProductController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult SelectBySubCategory(int id)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            foreach (TProduct p in _dbIgo.TProducts.Where(n => n.FSubCategoryId == id))
            {
                CProductViewModel product = new CProductViewModel(_dbIgo);
                product.product = p;
                products.Add(product);
            }
            return Json(products);
        }
        public IActionResult searchProduct(int SubcategoryID, string keyword)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            IEnumerable<TProduct> datas = _dbIgo.TProducts.Where(n => n.FSubCategoryId == SubcategoryID && n.FProductName.Contains(keyword));
            if (datas != null)
            {
                foreach (TProduct p in datas)
                {
                    CProductViewModel product = new CProductViewModel(_dbIgo);
                    product.product = p;
                    products.Add(product);
                }
                return Json(products);
            }
            return Json(null);
        }
        public IActionResult searchProductById(int Id)
        {
            TProduct data = _dbIgo.TProducts.Where(n => n.FProductId == Id).FirstOrDefault();
            if (data != null)
            {
                CProductViewModel product = new CProductViewModel(_dbIgo);
                product.product = data;
                return Json(product);
            }
            return Json(null);
        }
        public IActionResult Create(TProduct prod)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            _dbIgo.TProducts.Add(prod);
            _dbIgo.SaveChanges();
            CProductViewModel product = new CProductViewModel(_dbIgo);
            product.product = _dbIgo.TProducts.OrderBy(n => n.FProductId).Last();
            products.Add(product);
            return Json(products);
        }
        public IActionResult Delete(int id)
        {
            TProduct prod = _dbIgo.TProducts.Find(id);
            _dbIgo.TProducts.Remove(prod);
            _dbIgo.SaveChanges();
            return RedirectToAction("SelectBySubCategory", new { id = prod.FSubCategoryId });
        }
        public IActionResult Edit(TProduct prod)
        {
            TProduct p = _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == prod.FProductId);
            p.FProductName = prod.FProductName;
            p.FCityId = prod.FCityId;
            p.FAddress = prod.FAddress;
            p.FSubCategoryId = prod.FSubCategoryId;
            p.FSupplierId = prod.FSupplierId;
            p.FQuantity = prod.FQuantity;
            p.FStartTime = prod.FStartTime;
            p.FEndTime = prod.FEndTime;
            p.FIntroduction = prod.FIntroduction;
            _dbIgo.SaveChanges();

            List<CProductViewModel> products = new List<CProductViewModel>();
            foreach (TProduct item in _dbIgo.TProducts.Where(n => n.FSubCategoryId == prod.FSubCategoryId))
            {
                CProductViewModel product = new CProductViewModel(_dbIgo);
                product.product = item;
                products.Add(product);
            }
            return Json(products);
        }
    }
}
