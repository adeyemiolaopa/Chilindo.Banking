using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chilindo.Banking.Domain
{
    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; private set; }
        [Required]
        public int AccountNo { get; private set; }
        [Required]
        public string CurrencyID { get; private set; }
        [Required]
        public decimal Amount { get; private set; }
        [Required]
        public TransactionType TransactionType { get; private set; }
        //public bool Success { get; set; }
        public decimal ExchangeRate { get; private set; }
        public DateTime EntryDate { get; private set; }

        public virtual BankAccount BankAccount { get; set; }

        protected Transaction() { }

        public Transaction(int accountNo, decimal amount, Currency currency, TransactionType transactionType)
        {
            TransactionID = Guid.NewGuid();
            AccountNo = accountNo;
            CurrencyID = currency.CurrencyID;
            Amount = amount;
            TransactionType = transactionType;
            //Success = true;
            ExchangeRate = currency.ExchangeRate;
            EntryDate = DateTime.Now;
        }

    }

    public enum TransactionType
    {
        DEPOSIT = 1,
        WITHDRAWAL = 2
    }

    public enum TransactionStatus
    {
        SUCCESS = 1,
        FAILED = 2
    }
}
