using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TProductsPhoto
    {
        public int FProductPhotoId { get; set; }
        public int? FProductId { get; set; }
        public string FPhotoPath { get; set; }
        public int? FPhotoSiteId { get; set; }
        public int? FMovieId { get; set; }

        public virtual TPhotoSite FPhotoSite { get; set; }
        public virtual TProduct FProduct { get; set; }
    }
}
