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
    public class CCustomerViewModel
    {
        private TCustomer _cust;

        public CCustomerViewModel()
        {
            _cust = new TCustomer();
        }
        public TCustomer customer
        {
            get { return _cust; }
            set { _cust = value; }
        }

        [DisplayName("會員序號")]
        public int FCustomerId
        {
            get { return _cust.FCustomerId; }
            set { _cust.FCustomerId = value; }
        }

        [DisplayName("IGO帳號 (手機號碼)")]
        [Required(ErrorMessage = "不可為空")]
        [RegularExpression(@"^[0]{1}[9]{1}[0-9]{8}$", ErrorMessage = "手機格式有誤")]

        public string FPhone
        {
            get { return _cust.FPhone; }
            set { _cust.FPhone = value; }
        }


        [DisplayName("密碼")]
        [Required(ErrorMessage = "不可為空")]
        [RegularExpression(@"^[a-zA-Z0-9]{8,50}$", ErrorMessage = "密碼格式有誤")]
        public string FPassword
        {
            get { return _cust.FPassword; }
            set { _cust.FPassword = value; }
        }

        [DisplayName("Email")]
        [Required(ErrorMessage = "不可為空")]
        [StringLength(50, ErrorMessage = "不得超過50字元")]
        [EmailAddress(ErrorMessage = "Email格式有誤")]
        public string FEmail
        {
            get { return _cust.FEmail; }
            set { _cust.FEmail = value; }
        }

        [DisplayName("姓氏(LastName)")]
        [Required(ErrorMessage = "不可為空")]
        public string FLastName
        {
            get { return _cust.FLastName; }
            set { _cust.FLastName = value; }
        }

        [DisplayName("名字(FirstName)")]
        [Required(ErrorMessage = "不可為空")]
        public string FFirstName
        {
            get { return _cust.FFirstName; }
            set { _cust.FFirstName = value; }
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
        [DisplayName("地區")]
        public int? FCityId
        {
            get { return _cust.FCityId; }
            set { _cust.FCityId = value; }
        }

        [DisplayName("地址")]
        public string FAddress
        {
            get { return _cust.FAddress; }
            set { _cust.FAddress = value; }
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

        //public int FSupplierId 
        //{
        //    get {return _cust.FCustomerId; }
        //    set { _cust.FCustomerId = value; } 
        //}

        public IFormFile photo { get; set; }

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "不可為空")]
        [Compare("txtPassword", ErrorMessage = "")]
        public string firstPassword { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "不可為空")]
        [Compare("txtPassword", ErrorMessage = "新密碼不能與原本相同")]
        public string newPassword { get; set; }


        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "不可為空")]
        [Compare("txtPassword", ErrorMessage = "密碼不一致")]
        public string confirmPassword { get; set; }

        public CCustomerViewModel(TCustomer customer)
        {
            _cust = customer;

        }
    }


}
