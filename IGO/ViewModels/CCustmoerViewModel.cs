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
    public class CCustmoerViewModel
    {
        private TCustomer _cust;
        private DemoIgoContext _dbIgo;

        public CCustmoerViewModel(DemoIgoContext db)
        {
            _cust = new TCustomer();
            _dbIgo = db;
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

        [DisplayName("密碼")]
        [Required]
        public string FPassword
        {
            get { return _cust.FPassword; }
            set { _cust.FPassword = value; }
        }

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

        [DisplayName("姓氏")]
        [Required]
        public string FLastName
        {
            get { return _cust.FLastName; }
            set { _cust.FLastName = value; }
        }

        [DisplayName("名字")]
        [Required]
        public string FFirstName
        {
            get { return _cust.FFirstName; }
            set { _cust.FFirstName = value; }
        }

        [DisplayName("Email")]
        public string FEmail
        {
            get { return _cust.FEmail; }
            set { _cust.FEmail = value; }
        }

        [DisplayName("手機號碼")]
        [Required]
        public string FPhone
        {
            get { return _cust.FPhone; }
            set { _cust.FPhone = value; }
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
