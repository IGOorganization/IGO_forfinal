using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace IGO.Models
{
    public partial class TCustomer
    {
        public TCustomer()
        {
            TCollections = new HashSet<TCollection>();
            TFeedbackManagements = new HashSet<TFeedbackManagement>();
            TOrders = new HashSet<TOrder>();
            TShoppingCarts = new HashSet<TShoppingCart>();
            TTestResults = new HashSet<TTestResult>();
            TVouchers = new HashSet<TVoucher>();
        }

        [DisplayName("會員序號")]
        public int FCustomerId { get; set; }
        [DisplayName("密碼")]
        public string FPassword { get; set; }
        public int? FCityId { get; set; }
        [DisplayName("地址")]
        public string FAddress { get; set; }
        [DisplayName("姓氏")]
        public string FLastName { get; set; }
        [DisplayName("名字")]
        public string FFirstName { get; set; }
        [DisplayName("Email")]
        public string FEmail { get; set; }
        [DisplayName("手機號碼(IGO帳號)")]
        public string FPhone { get; set; }
        [DisplayName("性別")]
        public string FGender { get; set; }
        [DisplayName("生日")]
        public string FBirth { get; set; }
        [DisplayName("註冊日期")]
        public string FSignUpDate { get; set; }
        [DisplayName("頭貼路徑")]
        public string FUserPhoto { get; set; }
        public int? FSupplierId { get; set; }

        public virtual TCity FCity { get; set; }
        public virtual TSupplier FSupplier { get; set; }
        public virtual ICollection<TCollection> TCollections { get; set; }
        public virtual ICollection<TFeedbackManagement> TFeedbackManagements { get; set; }
        public virtual ICollection<TOrder> TOrders { get; set; }
        public virtual ICollection<TShoppingCart> TShoppingCarts { get; set; }
        public virtual ICollection<TTestResult> TTestResults { get; set; }
        public virtual ICollection<TVoucher> TVouchers { get; set; }
    }
}
