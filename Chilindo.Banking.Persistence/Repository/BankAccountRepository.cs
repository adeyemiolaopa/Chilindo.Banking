using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chilindo.Banking.Domain;
using Chilindo.Banking.Persistence.Context;
using Chilindo.Banking.Domain.Dto;
using System.Configuration;
using Chilindo.Banking.Domain.Exceptions;
using AutoMapper.EntityFramework;

namespace Chilindo.Banking.Persistence.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankingContext _db;

        public BankAccountRepository(BankingContext context)
        {
            _db = context;
        }

        public BankAccountDto Add(BankAccount bankAccount)
        {
            if (bankAccount == null) {
                throw new Exception();
            }

            _db.BankAccounts.Add(bankAccount);
            _db.SaveChanges();

            var newbankAccount = AutoMapper.Mapper.Map<BankAccount, BankAccountDto>(bankAccount);
            return newbankAccount;
        }

        public BankAccount FindByID(int accountNo)
        {
            var bankAccount = _db.BankAccounts.Find(accountNo);

            if (bankAccount == null) {
                throw new InValidAccountNoException("The account number you supplied is incorrect!");
            }

            //var bankAccountDto = AutoMapper.Mapper.Map<BankAccount, BankAccountDto>(bankAccount);
            return bankAccount;
        }

        public List<BankAccountDto> GetAll()
        {
            var bankAccounts = _db.BankAccounts.OrderBy(O => O.AccountNo).ToList();
            var bankAccountsDto = AutoMapper.Mapper.Map<List<BankAccount>, List<BankAccountDto>>(bankAccounts);
            return bankAccountsDto;
        }
    }
}
