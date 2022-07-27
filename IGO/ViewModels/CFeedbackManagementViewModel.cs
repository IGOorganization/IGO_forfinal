using IGO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CFeedbackManagementViewModel
    {
        private readonly DemoIgoContext _db;
        private TFeedbackManagement _feedbackManagement;
        public CFeedbackManagementViewModel(DemoIgoContext db)
        {
            _feedbackManagement = new TFeedbackManagement();
            _db = db;
        }
        public TFeedbackManagement feedbackManagement { get { return feedbackManagement; } set { _feedbackManagement = value; } }
        public int FFeedbackId { get { return _feedbackManagement.FFeedbackId; } set { _feedbackManagement.FFeedbackId = value; } }
        public int? FCustomerId { get { return _feedbackManagement.FCustomerId; } set { _feedbackManagement.FCustomerId = value; } }
        public string FFeedbackContent { get { return _feedbackManagement.FFeedbackContent; } set { _feedbackManagement.FFeedbackContent = value; } }
        public int? FRanking { get { return _feedbackManagement.FRanking; } set { _feedbackManagement.FRanking = value; } }
        public int? FProductId { get { return _feedbackManagement.FProductId; } set { _feedbackManagement.FProductId = value; } }
        public string FFeedbackDate { get { return _feedbackManagement.FFeedbackDate; } set { _feedbackManagement.FFeedbackDate = value; } }

        public virtual TCustomer FCustomer { get; set; }
        public virtual TProduct FProduct { get; set; }

        [DisplayName("顧客姓名")]
        public TCustomer customer { get { return _db.TCustomers.FirstOrDefault(c => c.FCustomerId == _feedbackManagement.FCustomerId); } }

        [DisplayName("產品名稱")]
        public TProduct product { get { return _db.TProducts.FirstOrDefault(c => c.FProductId == _feedbackManagement.FProductId); } }
        public string ProductImage
        {
            get
            {
                TProductsPhoto tp = _db.TProductsPhotos.FirstOrDefault(n => n.FProductId == FProductId && n.FPhotoSiteId == 3);
                if (tp != null)
                {
                    return tp.FPhotoPath;
                }
                return null;
            }
        }
    }
}
