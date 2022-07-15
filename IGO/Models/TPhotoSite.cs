using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TPhotoSite
    {
        public TPhotoSite()
        {
            TProductsPhotos = new HashSet<TProductsPhoto>();
        }

        public int FPhotoSiteId { get; set; }
        public string FPhotoplace { get; set; }

        public virtual ICollection<TProductsPhoto> TProductsPhotos { get; set; }
    }
}
