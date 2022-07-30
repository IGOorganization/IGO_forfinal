using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CAdminLiveViewModel
    {
        public int? fProductId { get; set; }
        public int? fTicketAndProductId { get; set; }
        public int supplier { get; set; }
        public string fProductName { get; set; }
        public int tickettype { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public string fIntroduction { get; set; }

    }
}
