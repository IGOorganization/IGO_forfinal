using IGO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CMovieViewModel
    {
        private TProductsPhoto productsPhoto;
        public CMovieViewModel()
        {
            productsPhoto =  new TProductsPhoto();
        }

        public TProductsPhoto theproductsPhoto
        {
            get { return productsPhoto; }
            set { productsPhoto = value; }
        }
        public string FImagePath
        {
            get { return productsPhoto.FPhotoPath; }
            set { productsPhoto.FPhotoPath = value; } 
       }
        public IFormFile photo { get; set; }
    }
}
