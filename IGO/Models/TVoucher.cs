using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TVoucher
    {
        public int FVoucherId { get; set; }
        public int? FCustomerId { get; set; }
        public string FVoucherNumber { get; set; }
        public decimal? FVoucherPrice { get; set; }
        public bool? FVoucherStatus { get; set; }
        public string FVoucherStartDate { get; set; }
        public string FVoucherEndDate { get; set; }
        public string FVoucherName { get; set; }

        public virtual TCustomer FCustomer { get; set; }
    }
}
