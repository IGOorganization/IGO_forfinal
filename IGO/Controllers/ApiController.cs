using IGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ApiController : Controller
    {
        private DemoIgoContext _dbIgo;
        public ApiController(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        //=======回傳縣市======
        public IActionResult getCity()
        {
            return Json(_dbIgo.TCities);
        }
        //=======回傳供應商======
        public IActionResult getSupplier(int subid,int cityid)
        {
            return Json(_dbIgo.TSuppliers.Where(n=>n.FSubCategoryId==subid&&n.FCityId==cityid));
        }
    }
}
