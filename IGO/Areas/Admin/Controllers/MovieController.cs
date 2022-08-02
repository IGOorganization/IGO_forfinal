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
        private readonly IWebHostEnvironment webHostEnvironment;
        public MovieController(DemoIgoContext db, IWebHostEnvironment environment)
        {
            this._igodb = db;
            webHostEnvironment = environment;
        }
        public IActionResult List(string searchName)
        {

            List<TMovie> movies = _igodb.TMovies.Select(x => x).ToList();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                movies = movies.Where(x => x.Cname.Contains(searchName) || x.Ename.Contains(searchName)).ToList();
            }

            ViewBag.SearchName = searchName;
            return View(movies);
        }

        public JsonResult CreateMovie(string Cname, string Ename, string Type, int Time, string Description)
        {
            var files = Request.Form.Files;
            List<string> photo = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = webHostEnvironment.WebRootPath + "/img/" + file.FileName;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    photo.Add(file.FileName);
                }
            }

            TMovie movie = new TMovie
            {
                Cname = Cname,
                Ename = Ename,
                Type = Type,
                Time = Time,
                Description = Description,
            };
            _igodb.TMovies.Add(movie);
            _igodb.SaveChanges();

            foreach (string path in photo)
            {
                TProductsPhoto productsPhoto = new TProductsPhoto
                {
                    FPhotoPath = path,
                    FMovieId = movie.MovieId
                };
                _igodb.TProductsPhotos.Add(productsPhoto);
            }
            _igodb.SaveChanges();

            return Json(true);
        }

        public JsonResult DeleteMovie(int movieID)
        {
            List<TProductsPhoto> moviePhoto = _igodb.TProductsPhotos.Where(x => x.FMovieId == movieID).ToList();
            foreach (var data in moviePhoto)
            {
                _igodb.TProductsPhotos.Remove(data);
            }

            var movie = _igodb.TMovies.FirstOrDefault(x => x.MovieId == movieID);
            _igodb.TMovies.Remove(movie);
            _igodb.SaveChanges();
            return Json(true);
        }

        public JsonResult Edit(int movieID)
        {
            List<CMovieViewModel> list = new List<CMovieViewModel>();
            List<TMovie> movie = _igodb.TMovies.Where(x => x.MovieId == movieID).ToList();
            foreach (TMovie tmovie in movie)
            {
                CMovieViewModel cm = new CMovieViewModel();
                cm.Movie = tmovie;
                var q = _igodb.TProductsPhotos.Where(x => x.FMovieId == movieID);
                cm.PhotoPathList = q.Select(x => x.FPhotoPath).ToList();
                list.Add(cm);
            }

            return Json(list);
        }

        public IActionResult deletephoto(string path)
        {
            TProductsPhoto tp = _igodb.TProductsPhotos.FirstOrDefault(n => n.FPhotoPath == path);
            _igodb.TProductsPhotos.Remove(tp);
            _igodb.SaveChanges();
            return Json(true);
        }

        public JsonResult EditMovie(int MovieID, string Cname, string Ename, string Type, int Time, string Description)
        {

            var files = Request.Form.Files;
            List<string> photo = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = webHostEnvironment.WebRootPath + "/img/" + file.FileName;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    photo.Add(file.FileName);
                }
            }

            TMovie movie = _igodb.TMovies.FirstOrDefault(x => x.MovieId == MovieID);
            movie.Cname = Cname;
            movie.Ename = Ename;
            movie.Type = Type;
            movie.Time = Time;
            movie.Description = Description;
            _igodb.SaveChanges();

            if (photo != null)
            {
                foreach (string path in photo)
                {
                    TProductsPhoto productsPhoto = new TProductsPhoto
                    {
                        FPhotoPath = path,
                        FMovieId = MovieID
                    };
                    _igodb.TProductsPhotos.Add(productsPhoto);
                }

                _igodb.SaveChanges();
            }



            return Json(true);
        }

    }

}
