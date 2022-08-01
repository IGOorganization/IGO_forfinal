using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCollection
    {
        public TCollection()
        {
            TCollectionGroupDetails = new HashSet<TCollectionGroupDetail>();
        }

        public int FCollectionId { get; set; }
        public int? FCustomerId { get; set; }
        public int? FProductId { get; set; }
        public string FCollectionDate { get; set; }
        public int? FMovieId { get; set; }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TMovie FMovie { get; set; }
        public virtual TProduct FProduct { get; set; }
        public virtual ICollection<TCollectionGroupDetail> TCollectionGroupDetails { get; set; }
    }
}
