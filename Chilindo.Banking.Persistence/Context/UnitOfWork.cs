using Chilindo.Banking.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Persistence.Context
{
    public class UnitOfWork : IDisposable
    {
        private readonly BankingContext _db;
        private readonly string _connectionString;
        public readonly CurrencyRepository _currencyRepository;
        public readonly BankAccountRepository _bankAccountRepository;
        public readonly TransactionRepository _transactionRepository;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            _db = new BankingContext(_connectionString);
            _bankAccountRepository = new BankAccountRepository(_db);
            _currencyRepository = new CurrencyRepository(_db);
            _transactionRepository = new TransactionRepository(_db);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
