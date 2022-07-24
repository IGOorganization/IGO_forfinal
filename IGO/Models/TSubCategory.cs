using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TSubCategory
    {
        public TSubCategory()
        {
            TProducts = new HashSet<TProduct>();
            TSuppliers = new HashSet<TSupplier>();
            TTicketTypes = new HashSet<TTicketType>();
        }

        public int FSubCategoryId { get; set; }
        public int? FCategoryId { get; set; }
        public string FSubCategoryName { get; set; }
        public string FImagePath { get; set; }

        public virtual TCategory FCategory { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
        public virtual ICollection<TSupplier> TSuppliers { get; set; }
        public virtual ICollection<TTicketType> TTicketTypes { get; set; }
    }
}
