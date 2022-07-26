using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ParkingController : Controller
    {
        public IActionResult Parking()
        {
            return View();
        }
    }
}
