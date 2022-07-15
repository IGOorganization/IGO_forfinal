using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCity
    {
        public TCity()
        {
            TCustomers = new HashSet<TCustomer>();
            TProducts = new HashSet<TProduct>();
        }

        public int FCityId { get; set; }
        public string FCityName { get; set; }

        public virtual ICollection<TCustomer> TCustomers { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
    }
}
