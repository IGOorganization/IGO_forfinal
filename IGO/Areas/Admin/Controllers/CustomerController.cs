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

        DemoIgoContext db = new DemoIgoContext();


        public IActionResult List(CKeywordViewModel vModel)
        {
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in db.TCustomers
                        select t;
            }
            else
            {
                datas = db.TCustomers.Where(t => t.FPhone.Contains(vModel.txtKeyword) ||
                    t.FLastName.Contains(vModel.txtKeyword) ||
                    t.FFirstName.Contains(vModel.txtKeyword) ||
                    t.FAddress.Contains(vModel.txtKeyword) ||
                    t.FEmail.Contains(vModel.txtKeyword) ||
                    t.FSignUpDate.Contains(vModel.txtKeyword) ||
                    t.FGender.Contains(vModel.txtKeyword));
                    
            }


            List<CCustomerViewModel> list = new List<CCustomerViewModel>();
            foreach (TCustomer item in datas)
            {
                CCustomerViewModel vmodel = new CCustomerViewModel();
                vmodel.customer = item;
                list.Add(vmodel);
            }

            return View(list);

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

            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == id);
            if (data == null)
                return RedirectToAction("List");
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(CCustomerViewModel vModel)
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
                cust.FCustomerId = vModel.FCustomerId;
                cust.FLastName = vModel.FLastName;
                cust.FFirstName = vModel.FFirstName;
                cust.FGender = vModel.FGender;
                cust.FPhone = vModel.FPhone;
                cust.FEmail = vModel.FEmail;
                cust.FAddress = vModel.FAddress;
                
            }
            _dbIgo.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {

            TCustomer data = _dbIgo.TCustomers.FirstOrDefault(t => t.FCustomerId == id);
            if (data != null)
            {
                _dbIgo.TCustomers.Remove(data);
                _dbIgo.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
