using Microsoft.AspNetCore.Http;
using IGO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjMvcCoreDemo.ViewModels
{
    public class CFeedbackProductViewModel
    {
        public CFeedbackProductViewModel()
        {
            _prod = new TFeedbackManagement();
        }
        private TFeedbackManagement _prod;
        public TFeedbackManagement product
        {
            get { return _prod; }
            set { _prod = value; }
        }
        public int FeedbackId
        {
            get { return _prod.FFeedbackId; }
            set { _prod.FFeedbackId = value; }
        }
        [DisplayName("客戶id")]
        public int? CustomerId
        {
            get { return _prod.FCustomerId; }
            set { _prod.FCustomerId = value; }
        }
        [DisplayName("評論")]
        public string FeedbackContent
        {
            get { return _prod.FFeedbackContent; }
            set { _prod.FFeedbackContent = value; }
        }
        [DisplayName("分數")]
        public int? Ranking
        {
            get { return _prod.FRanking; }
            set { _prod.FRanking = value; }
        }
        [DisplayName("產品id")]
        public int? ProductsId
        {
            get { return _prod.FProductId; }
            set { _prod.FProductId = value; }
        }
        [DisplayName("日期")]
        public string FeedbackDate
        {
            get { return _prod.FFeedbackDate; }
            set { _prod.FFeedbackDate = value; }

        }
        //public string ImagePath
        //{
        //    get { return _prod.ImagePath; }
        //    set { _prod.ImagePath = value; }
        //}
        public IFormFile photo { get; set; }
    }
}
