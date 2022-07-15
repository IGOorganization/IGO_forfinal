using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TSupplier
    {
        public TSupplier()
        {
            TProducts = new HashSet<TProduct>();
        }

        public int FSupplierId { get; set; }
        public string FCompanyName { get; set; }
        public string FPhone { get; set; }
        public string FAddress { get; set; }

        public virtual ICollection<TProduct> TProducts { get; set; }
    }
}
