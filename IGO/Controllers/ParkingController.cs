using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IWebHostEnvironment _host;
        public ParkingController(IWebHostEnvironment hostEnvironment)
        {
            _host = hostEnvironment;
        }
        public IActionResult Parking()
        {
            return View();
        }
        //7/27新增
        public IActionResult backupParking()
        {
            return View();
        }
        //7/27新增
        public IActionResult ReadJaonFile(string file)
        {
            string filename = file + ".json";
            string path = Path.Combine(_host.WebRootPath, "json", filename);
            string text = System.IO.File.ReadAllText(path, Encoding.UTF8);

            return Json(text);
        }
    }
}
