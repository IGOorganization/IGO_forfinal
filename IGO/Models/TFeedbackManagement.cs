using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TFeedbackManagement
    {
        public int FFeedbackId { get; set; }
        public int? FCustomerId { get; set; }
        public string FFeedbackContent { get; set; }
        public int? FRanking { get; set; }
        public int? FProductId { get; set; }
        public string FFeedbackDate { get; set; }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TProduct FProduct { get; set; }
    }
}
