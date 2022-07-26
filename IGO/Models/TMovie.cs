using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TMovie
    {
        public TMovie()
        {
            TCollections = new HashSet<TCollection>();
            TOrderDetails = new HashSet<TOrderDetail>();
            TProductsPhotos = new HashSet<TProductsPhoto>();
            TShoppingCarts = new HashSet<TShoppingCart>();
        }

        public int MovieId { get; set; }
        public string Cname { get; set; }
        public string Description { get; set; }
        public string Ename { get; set; }
        public string Type { get; set; }
        public int? Time { get; set; }

        public virtual ICollection<TCollection> TCollections { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TProductsPhoto> TProductsPhotos { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
    }
}
