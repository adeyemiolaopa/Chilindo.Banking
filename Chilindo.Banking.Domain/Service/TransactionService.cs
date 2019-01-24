using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Domain.Interfaces;
using Chilindo.Banking.Domain.AppHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Service
{
    public class TransactionService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IBankAccountRepository bankAccountRepository,
                                  ITransactionRepository transactionRepository,
                                  ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
        }

        public TransactionDto CreateDeposit(int accountNo, decimal amount, string currencyID)
        {
            var bankAccount = _bankAccountRepository.FindByID(accountNo);

            if (bankAccount is null) {
                throw new InValidAccountNoException("Your account number does not exist!");
            }

            if (amount <= 0) {
                throw new InValidAmountException("Your deposit amount cannot be equal to or less than 0!");
            }

            if (currencyID.Length < 3) {
                throw new InValidAmountException("Your currency is not in correct format. Pass your currencyID!");
            }

            var transactionCurrencyDto = _currencyRepository.FindByID(currencyID);

            if (transactionCurrencyDto is null) {
                throw new InValidCurrencyIDException("Your currency ID does not exist!");
            }

            var accountExchangeRate = _currencyRepository.FindByID(bankAccount.CurrencyID).ExchangeRate;

            var currency = AutoMapper.Mapper.Map<CurrencyDto, Currency>(transactionCurrencyDto);
            //var bankAccount = AutoMapper.Mapper.Map<BankAccountDto, BankAccount>(bankAccountDto);
            bankAccount.Deposit(accountNo, amount, currency, accountExchangeRate);

            var transactionDto = new TransactionDto();

            try {
                transactionDto = _transactionRepository.Add(bankAccount.Transactions.Last().TransactionID);
            }
            catch (Exception ex) {
                throw new Exception($"Something went wrong while processing your deposit. Try again later! {ex.Message}");
            }
            
            return transactionDto;
        }

        public TransactionDto CreateWithdrawal(int accountNo, decimal amount, string currencyID)
        {
            var bankAccount = _bankAccountRepository.FindByID(accountNo);

            if (bankAccount is null) {
                throw new InValidAccountNoException("Your account number does not exist!");
            }

            if (amount <= 0) {
                throw new InValidAmountException("Your withdrawal amount cannot be equal to or less than 0!");
            }

            if (currencyID.Length < 3) {
                throw new InValidAmountException("Your currency is not in correct format. Pass your currencyID!");
            }

            var currencyDto = _currencyRepository.FindByID(currencyID);

            if (currencyDto is null) {
                throw new InValidCurrencyIDException("Your currency ID does not exist!");
            }

            var accountExchangeRate = _currencyRepository.FindByID(bankAccount.CurrencyID).ExchangeRate;

            var currency = AutoMapper.Mapper.Map<CurrencyDto, Currency>(currencyDto);
            //var bankAccount = AutoMapper.Mapper.Map<BankAccountDto, BankAccount>(bankAccountDto);
            bankAccount.Withdraw(accountNo, amount, currency, accountExchangeRate);

            var transactionDto = new TransactionDto();
            
            try {
                transactionDto = _transactionRepository.Add(bankAccount.Transactions.Last().TransactionID);
            }
            catch (Exception) {
                throw new Exception("Something went wrong while processing your withdrawal. Try again later!");
            }
            
            return transactionDto;
        }

    }
}
