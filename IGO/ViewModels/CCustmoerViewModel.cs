using IGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IGO
{
    public class CCustmoerViewModel
    {
        private TCustomer _Cust;
        private DemoIgoContext _dbIgo;

        public CCustmoerViewModel(DemoIgoContext db)
        {
            _Cust = new TCustomer();
            _dbIgo = db;
        }
        public TCustomer customer
        {
            get { return _Cust; }
            set { _Cust = value; }
        }

        [DisplayName("會員序號")]
        public int FCustomerId 
        { 
            get { return _Cust.FCustomerId; }
            set { _Cust.FCustomerId = value; }
        }

        [DisplayName("密碼")]
        [Required]
        public string FPassword 
        {
            get { return _Cust.FPassword; }
            set { _Cust.FPassword = value; }
        }

        public int? FCityId 
        {
            get { return _Cust.FCityId; }
            set { _Cust.FCityId = value; }
        }

        [DisplayName("地址")]
        public string FAddress 
        {
            get { return _Cust.FAddress; }
            set { _Cust.FAddress = value; }
        }

        [DisplayName("姓氏")]
        [Required]
        public string FLastName
        {
            get { return _Cust.FLastName; }
            set { _Cust.FLastName = value; }
        }

        [DisplayName("名字")]
        [Required]
        public string FFirstName
        {
            get { return _Cust.FFirstName; }
            set { _Cust.FFirstName = value; }
        }

        [DisplayName("Email")]
        public string FEmail
        {
            get { return _Cust.FEmail; }
            set { _Cust.FEmail = value; }
        }

        [DisplayName("手機號碼")]
        [Required]
        public string FPhone
        {
            get { return _Cust.FPhone; }
            set { _Cust.FPhone = value; }
        }

        [DisplayName("性別")]
        public string FGender
        {
            get { return _Cust.FGender; }
            set { _Cust.FGender = value; }
        }

        [DisplayName("生日")]
        public string FBirth
        {
            get { return _Cust.FBirth; }
            set { _Cust.FBirth = value; }
        }

        [DisplayName("註冊日期")]
        public string FSignUpDate
        {
            get { return _Cust.FSignUpDate; }
            set { _Cust.FSignUpDate = value; }
        }

        [DisplayName("頭貼路徑")]
        public string FUserPhoto
        {
            get { return _Cust.FUserPhoto; }
            set { _Cust.FUserPhoto = value; }
        }
    }
}
