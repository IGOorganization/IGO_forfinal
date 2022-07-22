using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class CustomerController : Controller
    {
        private DemoIgoContext _dbIgo;
        public CustomerController(DemoIgoContext db)
        {
            _dbIgo = db;
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
                    t.FPhone.Contains(vModel.txtKeyword) ||
                    t.FEmail.Contains(vModel.txtKeyword) ||
                    t.FAddress.Contains(vModel.txtKeyword));
            }
            return View(datas);
        }

        public IActionResult Edit(int? id)
        {

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(c => c.FCustomerId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(TCustomer tc)
        {

            TCustomer cust = _dbIgo.TCustomers.FirstOrDefault(c => c.FCustomerId == tc.FCustomerId);
            if (cust != null)
            {
                cust.FLastName = tc.FLastName;
                cust.FFirstName = tc.FFirstName;
                cust.FPhone = tc.FPhone;
                cust.FEmail = tc.FEmail;
                cust.FAddress = tc.FAddress;
                cust.FPassword = tc.FPassword;
                _dbIgo.SaveChanges();
            }
            return RedirectToAction("List");
        }


    }
}
