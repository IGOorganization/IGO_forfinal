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
        private DemoIgoContext _igodb;
        public MovieController(DemoIgoContext db)
        {
            this._igodb = db;
        }
        public IActionResult Index(string searchName)
        {

            List<TMovie> movies = _igodb.TMovies.Select(x => x).ToList();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                movies = movies.Where(x => x.Cname.Contains(searchName) || x.Ename.Contains(searchName)).ToList();
            }

            ViewBag.SearchName = searchName;
            return View(movies);
        }






    }
}
