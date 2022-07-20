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
                var q = _dbIgo.TProductsPhotos.Where(p => p.FMovieId == t.MovieId);
                cm.PhotoPathList = q.Select(x => x.FPhotoPath).ToList();
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

        public IActionResult Detail(int ID)
        {
            var movie = _dbIgo.TMovies.Where(t => t.MovieId == ID).FirstOrDefault();
            var supplier = _dbIgo.TSuppliers.Where(t => t.FCompanyName.Contains("影城")).ToList();
            var showing = _dbIgo.TShowings.ToList();
            var seat = _dbIgo.TMovieSeats.ToList();
            var ticketType = _dbIgo.TMovieTicketTypes.ToList();

            List<CMovieSeatViewModel> List = new List<CMovieSeatViewModel>();

            CMovieViewModel cMovie = new CMovieViewModel();
            cMovie.Movie = movie;
            cMovie.PhotoPathList = _dbIgo.TProductsPhotos.Where(t => t.FMovieId == ID).Select(t => t.FPhotoPath).Skip(1).ToList();

            foreach (TMovieSeat data in seat)
            {
                CMovieSeatViewModel cm = new CMovieSeatViewModel();
                cm.seat = data;
                List.Add(cm);
            }

            return View(new Tuple<CMovieViewModel, List<TSupplier>, List<TShowing>, List<CMovieSeatViewModel>, List<TMovieTicketType>>(cMovie, supplier, showing, List, ticketType));
        }
    }
}
