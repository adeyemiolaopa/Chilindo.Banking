using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chilindo.Banking.Presentation.Models
{
    public class Account
    {
        //[Required]
        [DisplayName("Account Number")]
        public int AccountNumber { get; set; }
        public bool Successful { get; set; }
        public decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
        public decimal? Amount { get; set; }
    }
}