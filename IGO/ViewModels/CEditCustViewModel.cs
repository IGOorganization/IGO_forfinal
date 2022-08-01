using IGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CEditCustViewModel
    {
        private TCustomer _cust;

        public CEditCustViewModel()
        {
            _cust = new TCustomer();
        }
        public TCustomer customer
        {
            get { return _cust; }
            set { _cust = value; }
        }

        [DisplayName("手機號碼(IGO帳號)")]
        [Required(ErrorMessage = "請填寫手機號碼，不可為空白")]
        [RegularExpression(@"^[0]{1}[9]{1}[0-9]{8}$", ErrorMessage = "手機格式有誤，請正確填寫")]
        public string FPhone
        {
            get { return _cust.FPhone; }
            set { _cust.FPhone = value; }
        }

        [DisplayName("會員序號")]
        public int FCustomerId
        {
            get { return _cust.FCustomerId; }
            set { _cust.FCustomerId = value; }
        }

        [DisplayName("密碼")]
        [Required]
        public string FPassword
        {
            get { return _cust.FPassword; }
            set { _cust.FPassword = value; }
        }


        [DisplayName("地址")]
        public string FAddress
        {
            get { return _cust.FAddress; }
            set { _cust.FAddress = value; }
        }

        [DisplayName("姓氏")]
        [Required(ErrorMessage = "不可為空")]
        public string FLastName
        {
            get { return _cust.FLastName; }
            set { _cust.FLastName = value; }
        }

        [DisplayName("名字")]
        [Required(ErrorMessage = "不可為空")]
        public string FFirstName
        {
            get { return _cust.FFirstName; }
            set { _cust.FFirstName = value; }
        }

        [DisplayName("電子郵件")]
        [Required(ErrorMessage = "不可為空")]
        [StringLength(50, ErrorMessage = "不得超過50字元")]
        [EmailAddress(ErrorMessage = "Email格式有誤")]
        public string FEmail
        {
            get { return _cust.FEmail; }
            set { _cust.FEmail = value; }
        }       

        [DisplayName("性別")]
        public string FGender
        {
            get { return _cust.FGender; }
            set { _cust.FGender = value; }
        }

        [DisplayName("生日")]
        public string FBirth
        {
            get { return _cust.FBirth; }
            set { _cust.FBirth = value; }
        }

        [DisplayName("註冊日期")]
        public string FSignUpDate
        {
            get { return _cust.FSignUpDate; }
            set { _cust.FSignUpDate = value; }
        }

        [DisplayName("頭貼路徑")]
        public string FUserPhoto
        {
            get { return _cust.FUserPhoto; }
            set { _cust.FUserPhoto = value; }
        }

        public IFormFile photo {get;set;}

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string firstPassword { get; set; }
        public string confirmPassword { get; set; }
    }

    
}
