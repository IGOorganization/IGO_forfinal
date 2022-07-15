using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TSeat
    {
        public int FSeatId { get; set; }
        public int FProductId { get; set; }
        public string FSeatName { get; set; }
        public int? FOrderDetailId { get; set; }

        public virtual TOrderDetail FOrderDetail { get; set; }
        public virtual TProduct FProduct { get; set; }
    }
}
