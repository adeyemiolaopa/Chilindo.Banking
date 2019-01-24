using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Dto
{
    public class CurrencyDto
    {
        public string CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        //public bool Active { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal ExchangeRate { get; set; }
        //public List<BankAccount> Accounts { get; private set; }

    }
}
