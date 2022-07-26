using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CLoginViewModel
    {
        [Required]
        [DisplayName("IGO帳號 (手機號碼)")]
        public string txtAccount { get; set; }

        [Required]
        [DisplayName("密碼")]
        public string txtPassword { get; set; }
    }
}
