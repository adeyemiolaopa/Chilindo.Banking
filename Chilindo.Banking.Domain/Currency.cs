using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chilindo.Banking.Domain
{
    public class Currency
    {
        [Key, StringLength(3, MinimumLength = 3)]
        public string CurrencyID { get; private set; }
        [Required]
        public string CurrencyName { get; private set; }
        public DateTime EntryDate { get; private set; }
        public decimal ExchangeRate { get; private set; }

        //public virtual List<BankAccount> Accounts { get; private set; }

        internal Currency() { }

        public Currency(string currencyID, string currencyName, decimal exchangeRate)
        {
            CurrencyID = currencyID;
            CurrencyName = currencyName;
            ExchangeRate = exchangeRate;
            EntryDate = DateTime.Now;
        }
    }
}
