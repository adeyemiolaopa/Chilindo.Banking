using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chilindo.Banking.Domain
{
    public class BankAccount
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int AccountNo { get; private set; }
        [Required]
        public string AccountName { get; private set; }
        [Required]
        public string CurrencyID { get; private set; }
        [Required]
        [ConcurrencyCheck]
        public decimal Balance { get; private set; } //  
        public DateTime EntryDate { get; private set; }

        //public virtual Currency Currency { get; private set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        protected BankAccount() {  }

        public BankAccount(int accountNo, string accountName, Currency currency, decimal initialBalance)
        {
            AccountNo = accountNo;
            AccountName = accountName;
            CurrencyID = currency.CurrencyID;
            Balance = initialBalance;
            EntryDate = DateTime.Now;

            Transactions = new List<Transaction>();

            if (initialBalance > 0) {
                var transaction = new Transaction(accountNo, initialBalance, currency, TransactionType.DEPOSIT);
                Transactions.Add(transaction);
            }            
        }

        public void Deposit(int accountNo, decimal amount, Currency currency, decimal accountExchangeRate)
        {
            if (!CurrencyID.Equals(currency.CurrencyID)) {
                amount = AppHelper.AppHelper.CalculateAmountWithRates(amount, accountExchangeRate, currency.ExchangeRate);
            }

            var transaction = new Transaction(accountNo, amount, currency, TransactionType.DEPOSIT);
            this.Transactions.Add(transaction);
            Balance += amount;
        }

        public void Withdraw(int accountNo, decimal amount, Currency currency, decimal accountExchangeRate)
        {
            if (!CurrencyID.Equals(currency.CurrencyID)) {
                amount = AppHelper.AppHelper.CalculateAmountWithRates(amount, accountExchangeRate, currency.ExchangeRate);
            }

            if (amount > Balance) {
                throw new InsufficientBalanceException("The amount to withdraw cannot be greater than your account balance!");
            }

            var transaction = new Transaction(accountNo, amount, currency, TransactionType.WITHDRAWAL);
            Transactions.Add(transaction);
            Balance -= amount;
        }

    }

 


}
