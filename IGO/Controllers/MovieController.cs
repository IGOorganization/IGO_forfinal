using IGO.Models;
using IGO.ViewModels;
using Microsoft.AspNetCore.Http;
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

        public IActionResult List(string movieName)
        {
            List<CMovieViewModel> List = new List<CMovieViewModel>();

            //從DB拿出Movie資料
            List<TMovie> dbList = _dbIgo.TMovies.ToList();
            if (!string.IsNullOrWhiteSpace(movieName))
            {
                dbList = dbList.Where(x => x.Cname.Contains(movieName) || x.Ename.Contains(movieName)).ToList();
            }


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

        public IActionResult Detail(int? ID,int? row=10,int? col=10,int? dataId=0)
        {
            int userid = 0;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_LOGINED_USER);
            }
            if (ID == null)
            {
                ID = dataId;
            }
            if (row > 20)
            {
                row = 20;
            }
            if (col > 20)
            {
                col = 20;
            }
            var movie = _dbIgo.TMovies.Where(t => t.MovieId == ID).FirstOrDefault();
            var supplier = _dbIgo.TSuppliers.Where(t => t.FCompanyName.Contains("影城")).ToList();
            var showing = _dbIgo.TShowings.ToList();
            var seat = _dbIgo.TMovieSeats.ToList();
            var ticketType = _dbIgo.TMovieTicketTypes.ToList();
            ViewBag.row = row;
            ViewBag.col = col;

            List<CMovieSeatViewModel> List = new List<CMovieSeatViewModel>();

            CMovieViewModel cMovie = new CMovieViewModel();
            cMovie.Movie = movie;
            //cMovie.IsCollection = _dbIgo.TCollections.Where(t => t.FMovieId == ID && t.FCustomerId == userid).Count() > 0;
            cMovie.IsCollection = _dbIgo.TCollections.Any(t => t.FMovieId == ID && t.FCustomerId == userid);
            cMovie.PhotoPathList = _dbIgo.TProductsPhotos.Where(t => t.FMovieId == ID).Select(t => t.FPhotoPath).Skip(1).ToList();

            foreach (TMovieSeat data in seat)
            {
                CMovieSeatViewModel cm = new CMovieSeatViewModel();
                cm.seat = data;
                List.Add(cm);
            }




            return View(new Tuple<CMovieViewModel, List<TSupplier>, List<TShowing>, List<CMovieSeatViewModel>, List<TMovieTicketType>, int>(cMovie, supplier, showing, List, ticketType, userid));
        }

        public JsonResult CancelCollection(int customerID, int movieID)
        {
            //TCollection collection = _dbIgo.TCollections.Where(t => t.FMovieId == movieID && t.FCustomerId == customerID).FirstOrDefault();
            TCollection collection = _dbIgo.TCollections.FirstOrDefault(t => t.FMovieId == movieID && t.FCustomerId == customerID);
            _dbIgo.Remove(collection);
            _dbIgo.SaveChanges();
            return Json(true);
        }

        public JsonResult AddCollection(int customerID, int movieID)
        {
            TCollection collection = new TCollection
            {
                FCustomerId = customerID,
                FMovieId = movieID,
                FCollectionDate = DateTime.Now.ToString()
            };

            _dbIgo.Add(collection);
            _dbIgo.SaveChanges();

            return Json(true);
        }

        public JsonResult AddShoppingCart(int userID, int money, string bookingTime, int supplierID, int showingID, int movieID, string movieSeat, int ticketTypeID)
        {
            TMovie movie = _dbIgo.TMovies.FirstOrDefault(x => x.MovieId == movieID);
            TSupplier supplier = _dbIgo.TSuppliers.FirstOrDefault(x => x.FSupplierId == supplierID);
            TProduct product = new TProduct
            {
                FProductName = movie.Cname,
                FCityId = supplier.FCityId,
                FStartTime = "2022-10-22",
                FEndTime = "2022-10-22",
                FQuantity = 30,
                FAddress = supplier.FAddress,
                FSupplierId = supplierID,
            };
            _dbIgo.TProducts.Add(product);
            _dbIgo.SaveChanges();
            Random ran = new Random();
            string s = (ran.Next(1, 1000) * ran.Next(1, 1000)).ToString();
            List<int> movieSeats = movieSeat.Split('、').Select(x => int.Parse(x)).ToList();
            foreach (int movieSeatID in movieSeats)
            {
                TShoppingCart shoppingCart = new TShoppingCart
                {
                    FProductId = product.FProductId,
                    FCustomerId = userID,
                    FQuantity = 1,
                    FTotalPrice = money / movieSeats.Count,
                    FBookingTime = bookingTime,
                    FSupplierId = supplierID,
                    FShowingId = showingID,
                    FMovieId = movieID,
                    FMovieSeatId = movieSeatID,
                    FMovieTicketTypeId = ticketTypeID,
                    FTempOrder = s
                };
                _dbIgo.TShoppingCarts.Add(shoppingCart);
                _dbIgo.SaveChanges();
            }



            return Json(true);
        }
        public JsonResult SearchChosenSeat(int movieID, string movieDate, int supplierID, int showingID)
        {
            List<TShoppingCart> shoppingCarts = _dbIgo.TShoppingCarts.Where(t => t.FMovieId == movieID &&
                                                                                                                                                          t.FBookingTime == movieDate &&
                                                                                                                                                          t.FSupplierId == supplierID &&
                                                                                                                                                          t.FShowingId == showingID).ToList();

            List<TOrderDetail> orderDetails = _dbIgo.TOrderDetails.Where(t => t.FMovieId == movieID &&
               t.FBookingTime == movieDate && t.FSupplierId == supplierID && t.FShowingId == showingID).ToList();

            List<int> seatIDs = shoppingCarts.Select(t => t.FMovieSeatId ?? 0).ToList();

            //List<int> orderSeatIDs = orderDetails.Select(x => x.FMovieSeatId ?? 0).ToList();
            //seatIDs.AddRange(orderSeatIDs);
            seatIDs.AddRange(orderDetails.Select(x => x.FMovieSeatId ?? 0).ToList());

            List<TMovieSeat> seats = _dbIgo.TMovieSeats.Where(t => seatIDs.Contains(t.FSeatId)).ToList();

            List<string> result = seats.Select(x => x.FSeatRow + x.FSeatColumn).ToList();

            return Json(result);
        }

        public JsonResult ChangAddress(int supplierID)
        {
            if (supplierID == 0)
            { return Json(' '); }
            string Address = (_dbIgo.TSuppliers.FirstOrDefault(x => x.FSupplierId == supplierID)).FAddress.ToString();
            return Json(Address);
        }

        public JsonResult SeatChanged(int row, int col)
        {
            ViewBag.row = row;
            ViewBag.col = col;
            return Json(true);
        }
    }
}
