using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chilindo.Banking.Domain;
using Chilindo.Banking.Persistence.Context;
using Chilindo.Banking.Domain.Dto;

namespace Chilindo.Banking.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _db;

        public TransactionRepository(BankingContext context)
        {
            _db = context;
            //_db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public TransactionDto Add(Guid transactionID)
        {
            if (string.IsNullOrWhiteSpace(transactionID.ToString())) {
                throw new Exception("The TransactionID cannot be null");
            }
           
            _db.SaveChanges();

            var transactionDto = FindByID(transactionID);
            return transactionDto;
        }

        public TransactionDto FindByID(Guid transactionID)
        {
            var transaction = _db.Transactions.Find(transactionID);

            if (transaction == null) {
                throw new Exception($"Incorrect TransactionID! {transactionID}");
            }

            var newTransaction = AutoMapper.Mapper.Map<Transaction, TransactionDto>(transaction);
            return newTransaction;
        }

        public List<TransactionDto> GetAll()
        {
            var transactions = _db.Transactions.OrderBy(O => O.TransactionID).ToList();
            var newTransactions = AutoMapper.Mapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
            return newTransactions;
        }
    }
}
