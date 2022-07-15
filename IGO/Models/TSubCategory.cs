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
            TTicketTypes = new HashSet<TTicketType>();
        }

        public int FSubCategoryId { get; set; }
        public int? FCategoryId { get; set; }
        public string FSubCategoryName { get; set; }

        public virtual TCategory FCategory { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
        public virtual ICollection<TTicketType> TTicketTypes { get; set; }
    }
}
