using Comment.ViewModels;
using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CustomerController : Controller
    {
        private readonly DemoIgoContext _dbIgo;

        private IWebHostEnvironment _enviroment;


        public CustomerController(DemoIgoContext db, IWebHostEnvironment IGO)
        {
            _dbIgo = db;
            _enviroment = IGO;
        }



        public IActionResult List(CKeywordViewModel vModel)
        {
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in _dbIgo.TCustomers
                        select t;
            }
            else
            {
                datas = _dbIgo.TCustomers.Where(t => 
                    t.FLastName.Contains(vModel.txtKeyword) ||
                    t.FFirstName.Contains(vModel.txtKeyword) ||
                    t.FGender.Contains(vModel.txtKeyword) ||
                    t.FPhone.Contains(vModel.txtKeyword) ||
                    t.FEmail.Contains(vModel.txtKeyword) ||
                    t.FAddress.Contains(vModel.txtKeyword));
            }
            return View(datas);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tCustomers = await _dbIgo.TCustomers
                .FirstOrDefaultAsync(t => t.FCustomerId == id);
            if (tCustomers == null)
            {
                return NotFound();
            }

            return View(tCustomers);
        }


        public IActionResult Edit(int? id)
        {

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(CCustmoerViewModel vModel)
        {

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == vModel.FCustomerId);
            if (cust != null)
            {
                if (vModel.FUserPhoto != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    vModel.photo.CopyTo(new FileStream( _enviroment.WebRootPath+"/img/"+ photoName, FileMode.Create));
                    cust.FUserPhoto = photoName;
                }
                cust.FLastName = vModel.FLastName;
                cust.FFirstName = vModel.FFirstName;
                cust.FGender = vModel.FGender;
                cust.FPhone = vModel.FPhone;
                cust.FPassword = vModel.FPassword;
                cust.FEmail = vModel.FEmail;
                cust.FAddress = vModel.FAddress;
                
            }
            _dbIgo.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
