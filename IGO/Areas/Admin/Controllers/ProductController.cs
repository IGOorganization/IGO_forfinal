using IGO.Models;
using IGO.ViewModels;
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
    public class ProductController : Controller
    {
        DemoIgoContext _dbIgo;
        IWebHostEnvironment _enviroment;
        public ProductController(DemoIgoContext db, IWebHostEnvironment e)
        {
            _dbIgo = db;
            _enviroment = e;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Pages(int number)
        {
            IEnumerable<TProduct> datas = _dbIgo.TProducts.Where(n => n.FSubCategoryId == number);
            int num = datas.ToList().Count();
            decimal page = Math.Ceiling((decimal)num / 10);
            return Json(Convert.ToInt32(page));
        }
        public IActionResult SelectBySubCategory(int id, int num)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            IEnumerable<TProduct> datas = _dbIgo.TProducts.Where(n => n.FSubCategoryId == id);
            for (int i = num * 10; i < ((num + 1) * 10); i++)
            {
                if (i < datas.ToList().Count())
                {
                    CProductViewModel product = new CProductViewModel(_dbIgo);
                    product.product = datas.ToList()[i];
                    products.Add(product);
                }
            }
            string result = System.Text.Json.JsonSerializer.Serialize(products);

            return Json(result);
        }
        public IActionResult searchProduct(int SubcategoryID, string keyword)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            if (keyword != null)
            {
                IEnumerable<TProduct> datas = _dbIgo.TProducts.Where(n => n.FSubCategoryId == SubcategoryID && n.FProductName.Contains(keyword));
                if (datas != null)
                {
                    foreach (TProduct p in datas.ToList())
                    {
                        CProductViewModel product = new CProductViewModel(_dbIgo);
                        product.product = p;
                        products.Add(product);
                    }
                    string result = System.Text.Json.JsonSerializer.Serialize(products);
                    return Json(result);
                }
            }
            else
            {
                return RedirectToAction("SelectBySubCategory", new { id = SubcategoryID, num = 0 });
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
        public IActionResult Create(TProduct prod, IFormFile Photo)
        {
            List<CProductViewModel> products = new List<CProductViewModel>();
            _dbIgo.TProducts.Add(prod);
            TProductsPhoto tp = new TProductsPhoto()
            {
                FProductId = prod.FProductId,
                FPhotoSiteId = 3,
            };

            if (Photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Photo.CopyTo(new FileStream(_enviroment.WebRootPath + "/img/" + pName, FileMode.Create));
                tp.FPhotoPath = pName;
            }
            _dbIgo.TProductsPhotos.Add(tp);

            _dbIgo.SaveChanges();
            CProductViewModel product = new CProductViewModel(_dbIgo);
            product.product = _dbIgo.TProducts.OrderBy(n => n.FProductId).Last();
            product.fPhotoPath = tp.FPhotoPath;
            products.Add(product);
            string result = System.Text.Json.JsonSerializer.Serialize(products);
            return Json(result);
        }
        public IActionResult Delete(int id)
        {
            TProduct prod = _dbIgo.TProducts.Find(id);
            _dbIgo.TProducts.Remove(prod);

            _dbIgo.SaveChanges();
            return RedirectToAction("SelectBySubCategory", new { id = prod.FSubCategoryId });
        }
        public IActionResult Edit(TProduct prod, IFormFile Photo)
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

            TProductsPhoto tp = _dbIgo.TProductsPhotos.FirstOrDefault(n => n.FPhotoSiteId == 1 && n.FProductId == prod.FProductId);


            if (Photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Photo.CopyTo(new FileStream(_enviroment.WebRootPath + "/img/" + pName, FileMode.Create));
                if (tp == null)
                {
                    tp = new TProductsPhoto()
                    {
                        FProductId = prod.FProductId,
                        FPhotoSiteId = 3,
                        FPhotoPath = pName
                    };
                    _dbIgo.TProductsPhotos.Add(tp);
                }
                else
                    tp.FPhotoPath = pName;
            }


            _dbIgo.SaveChanges();

            List<CProductViewModel> products = new List<CProductViewModel>();
            foreach (TProduct item in _dbIgo.TProducts.Where(n => n.FProductId == prod.FProductId).ToList())
            {
                CProductViewModel product = new CProductViewModel(_dbIgo);
                product.product = item;
                product.fPhotoPath = tp.FPhotoPath;
                products.Add(product);
            }
            string result = System.Text.Json.JsonSerializer.Serialize(products);
            return Json(result);
        }
    }
}
