using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Areas.Admin.Controllers
{
    public class Comment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
