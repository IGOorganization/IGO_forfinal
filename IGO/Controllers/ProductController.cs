using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult FoodView()
        {
            return View();
        }
        public IActionResult ExhibitionView()
        {
            return View();
        }
        public IActionResult ViewPointView()
        {
            return View();
        }
    }
}
