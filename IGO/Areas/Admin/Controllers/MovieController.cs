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
    public class MovieController : Controller
    {
        private IWebHostEnvironment host { get; set; }
        public MovieController(IWebHostEnvironment p)
        {
            this.host = p;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePicture()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePicture(TProductsPhoto photo,CMovieViewModel p)
        {
            //string photoName = Guid.NewGuid().ToString() + ".jpg";
            //photo.FPhotoPath = "~/img/" + photoName;

            //p.photo.CopyTo(new FileStream(this.host.WebRootPath + @"\img\" + photoName, FileMode.Create));
            //DemoIgoContext db = new DemoIgoContext();
            //db.TProductsPhotos.Add(photo);
            //db.SaveChanges();
            return View();
        }
    }
}
