using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TTicketType
    {
        public TTicketType()
        {
            TOrderDetails = new HashSet<TOrderDetail>();
            TTicketAndProducts = new HashSet<TTicketAndProduct>();
        }

        public int FTicketId { get; set; }
        public string FTicketName { get; set; }
        public int? FSubCategoryId { get; set; }

        public virtual TSubCategory FSubCategory { get; set; }
        public virtual ICollection<TOrderDetail> TOrderDetails { get; set; }
        public virtual ICollection<TTicketAndProduct> TTicketAndProducts { get; set; }
    }
}
