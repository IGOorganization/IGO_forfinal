using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class productInfo
    {
        DemoIgoContext _dbIgo;
        public productInfo(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public int productid;
        public int? movieid;
        public string ProductName { get { return _dbIgo.TProducts.Find(productid).FProductName; } }
        public string PhotoPath
        {
            get
            { 
                if (movieid > 0)
                {
                    return _dbIgo.TProductsPhotos.FirstOrDefault(c => c.FMovieId == movieid).FPhotoPath;
                }
                if (_dbIgo.TProductsPhotos.FirstOrDefault(n => n.FProductId == productid && n.FPhotoSiteId == 3) == null)
                {
                    return null;
                }
                return _dbIgo.TProductsPhotos.FirstOrDefault(n => n.FProductId == productid && n.FPhotoSiteId == 3).FPhotoPath;
            }
        }
        public string Introduction { get 
            
            {
                if (movieid > 0)
                {
                    return _dbIgo.TMovies.FirstOrDefault(c => c.MovieId == movieid).Description;
                }

                return _dbIgo.TProducts.Find(productid).FIntroduction; 
            
            } }
    }
}
