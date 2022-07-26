using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCollection
    {
        public int FCollectionId { get; set; }
        public int? FCustomerId { get; set; }
        public int? FProductId { get; set; }
        public bool? FCollectionState { get; set; }
        public string FCollectionDate { get; set; }
        public int? FMovieId { get; set; }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TMovie FMovie { get; set; }
        public virtual TProduct FProduct { get; set; }
    }
}
