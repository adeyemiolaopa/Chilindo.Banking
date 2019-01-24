using Chilindo.Banking.Domain.Service;
using Chilindo.Banking.Persistence.Context;
using Chilindo.Banking.Persistence.Repository;
using Chilindo.Banking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Chilindo.Banking.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly string _connectionString;
        private readonly UnitOfWork _unitOfWork;
        private BankAccountService _bankAccountService;
        private TransactionService _transactionService;

        public AccountController() : base()
        {
            _connectionString = ConfigurationManager
                                            .ConnectionStrings["ChilindoBankingConnString"]
                                                   .ConnectionString;

            _unitOfWork = new UnitOfWork(_connectionString);
            _bankAccountService = new BankAccountService(_unitOfWork._bankAccountRepository, 
                                                         _unitOfWork._currencyRepository);

            _transactionService = new TransactionService(_unitOfWork._bankAccountRepository, 
                                                         _unitOfWork._transactionRepository, 
                                                         _unitOfWork._currencyRepository);
        }

        /// <summary>  
        /// Gets the account balance of the account number.  
        /// </summary>  
        /// <param name="accountNumber"></param>  
        /// <returns></returns>  
        /// uri: api/account/balance
        [HttpGet]
        public BankAccountDto Balance(int accountNumber)
        {
            try
            {
                var bankAccount = _bankAccountService.GetByID(accountNumber);

                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = bankAccount.AccountNo,
                    Balance = decimal.Round(bankAccount.Balance, 2, MidpointRounding.AwayFromZero),
                    Currency = bankAccount.CurrencyID,
                    Message = $"Your current balance is {bankAccount.Balance} {bankAccount.CurrencyID}!",
                    Successful = true
                };

                return bankAccountDto;
            }
            catch (Exception ex)
            {
                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = accountNumber,
                    Balance = null,
                    Currency = null,
                    Message = ex.Message,
                    Successful = false
                };

                return bankAccountDto;
            }

        }


        /// <summary>  
        /// Deposits the amount in the BankAccount object using the currency.  
        /// </summary>  
        /// <param name="bankAccount"></param>  
        /// <returns></returns>  
        /// uri: api/account/deposit
        [HttpPost]
        public BankAccountDto Deposit(BankAccountDto bankAccount)
        {
            try
            {
                _transactionService.CreateDeposit(bankAccount.AccountNumber, bankAccount.Amount, bankAccount.Currency);
                var currBankAccount = _bankAccountService.GetByID(bankAccount.AccountNumber);

                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = currBankAccount.AccountNo,
                    Balance = decimal.Round(currBankAccount.Balance, 2, MidpointRounding.AwayFromZero),
                    Currency = currBankAccount.CurrencyID,
                    Message = $"{bankAccount.Amount} {bankAccount.Currency} was successfully deposited into your account. Your current balance is {currBankAccount.Balance} {currBankAccount.CurrencyID}!",
                    Successful = true,
                    Amount = bankAccount.Amount
                };

                return bankAccountDto;
            }
            catch (Exception ex)
            {
                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = bankAccount.AccountNumber,
                    Balance = null,
                    Currency = null,
                    Message = ex.Message,
                    Successful = false,
                    Amount = bankAccount.Amount
                };

                return bankAccountDto;
            }

        }


        /// <summary>  
        /// Withdraws the amount in the BankAccount object using the currency.  
        /// </summary>  
        /// <param name="bankAccount"></param>  
        /// <returns></returns>  
        /// uri: api/account/withdraw
        [HttpPost]
        public BankAccountDto Withdraw(BankAccountDto bankAccount)
        {
            try
            {
                _transactionService.CreateWithdrawal(bankAccount.AccountNumber, bankAccount.Amount, bankAccount.Currency);
                var currBankAccount = _bankAccountService.GetByID(bankAccount.AccountNumber);

                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = currBankAccount.AccountNo,
                    Balance = decimal.Round(currBankAccount.Balance, 2, MidpointRounding.AwayFromZero),
                    Currency = currBankAccount.CurrencyID,
                    Message = $"{bankAccount.Amount} {bankAccount.Currency} was successfully withdrawn from your account. Your current balance is {currBankAccount.Balance} {currBankAccount.CurrencyID}!",
                    Successful = true,
                    Amount = bankAccount.Amount
                };

                return bankAccountDto;
            }
            catch (Exception ex)
            {
                var bankAccountDto = new BankAccountDto()
                {
                    AccountNumber = bankAccount.AccountNumber,
                    Balance = null,
                    Currency = null,
                    Message = ex.Message,
                    Successful = false,
                    Amount = bankAccount.Amount
                };

                return bankAccountDto;
            }
        }

    }
}
