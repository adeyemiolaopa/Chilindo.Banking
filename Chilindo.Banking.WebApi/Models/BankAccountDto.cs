using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Chilindo.Banking.WebApi.Models
{

    public class BankAccountDto
    {
        [DisplayName("Account Number")]
        public int AccountNumber { get; set; }
        public bool Successful { get; set; }
        public decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }
    }
}