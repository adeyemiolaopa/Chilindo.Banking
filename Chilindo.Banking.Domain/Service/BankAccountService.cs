using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Service
{
    public class BankAccountService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository, ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public BankAccountDto CreateAccount(int accountNo, string accountName, string currencyID, decimal initialBalance)
        {
            if (currencyID.Length < 3) {
                throw new InValidAmountException("Your currency is not in correct format. Pass your currencyID!");
            }

            var currencyDto = _currencyRepository.FindByID(currencyID);

            if (currencyDto is null) {
                throw new InValidCurrencyIDException("Your currency ID does not exist!");
            }

            var currency = AutoMapper.Mapper.Map<CurrencyDto, Currency>(currencyDto);
            var bankAccount = new BankAccount(accountNo, accountName, currency, initialBalance);

            var bankAccountDto = _bankAccountRepository.Add(bankAccount);
            return bankAccountDto;
        }

        public BankAccount GetByID(int accountNo)
        {
            var account = _bankAccountRepository.FindByID(accountNo);
            //var account = AutoMapper.Mapper.Map<BankAccountDto, BankAccount>(accountDto);
            return account;
        }
    }
}
