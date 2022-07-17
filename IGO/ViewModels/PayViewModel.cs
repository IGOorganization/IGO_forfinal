using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class PayViewModel
    {
    }
	public class SendToNewebPayIn
	{
		public string ChannelID { get; set; }
		public string MerchantID { get; set; }
		public string MerchantOrderNo { get; set; }
		public string ItemDesc { get; set; }
		public string Amt { get; set; }
		public string ExpireDate { get; set; }
		public string ReturnURL { get; set; }
		public string CustomerURL { get; set; }
		public string NotifyURL { get; set; }
		public string ClientBackURL { get; set; }
		public string Email { get; set; }
	}

	public class SendToNewebPayOut
	{
		public string MerchantID { get; set; }
		public string Version { get; set; }
		public string TradeInfo { get; set; }
		public string TradeSha { get; set; }
	}
}
