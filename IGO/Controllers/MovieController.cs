using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.Controllers
{
    public class MovieController : Controller
    {
        private DemoIgoContext _dbIgo;
        public MovieController(DemoIgoContext dbIgo)
        {
            _dbIgo = dbIgo;
        }

        public IActionResult List()
        {
            List<CMovieViewModel> List = new List<CMovieViewModel>();

            //從DB拿出Movie資料
            List<TMovie> dbList = _dbIgo.TMovies.ToList();

            //逐筆轉換成CMovieViewModel格式
            foreach (TMovie t in dbList)
            {
                CMovieViewModel cm = new CMovieViewModel();
                cm.Movie = t;
                List.Add(cm);
               // ---------------------------------------------------------------------
                //List.Add(new CMovieViewModel { Movie = t });
            }

            //List<CMovieViewModel> newList = dbList.Select(t => new CMovieViewModel
            //{
            //    Movie = t
            //}).ToList();

            return View(List);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
