using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Dto
{
    public class TransactionDto
    {
        public Guid TransactionID { get; set; }
        public int AccountNo { get; set; }
        public string CurrencyID { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        //public bool Success { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
