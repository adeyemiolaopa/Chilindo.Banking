using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Dto
{
    public class BankAccountDto
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; }
        public string CurrencyID { get; set; }
        public decimal Balance { get; set; }
        public DateTime EntryDate { get; set; }
        //public Currency Currency { get; private set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
