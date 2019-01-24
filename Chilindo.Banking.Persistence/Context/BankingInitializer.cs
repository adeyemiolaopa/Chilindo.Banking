using Chilindo.Banking.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Persistence.Context
{
    class BankingInitializer : CreateDatabaseIfNotExists<BankingContext>
    {
        protected override void Seed(BankingContext context)
        {
            // add default Currencies
            var currencies = new List<Currency>()
            {
                new Currency("NGN", "NAIRA", 364.50M), 
                new Currency("USD", "DOLLAR", 1),
                new Currency("GBP", "POUND", 0.78M),
                new Currency("EUR", "EURO", 0.88M),
                new Currency("TBT", "BAHT", 32.85M)
            };

            // add default Accounts
            var accounts = new List<BankAccount>()
            {
                new BankAccount(1277785430, "Adeyemi Olaopa", currencies[2], 400),
                new BankAccount(1344166695, "Fatai Afolabi", currencies[1], 200),
                new BankAccount(1490887064, "Nelson Johnson", currencies[0], 0),
                new BankAccount(1567579065, "Michael Peters", currencies[3], 0)
            };

            context.Currencies.AddRange(currencies);
            context.BankAccounts.AddRange(accounts);

            base.Seed(context);
            context.SaveChanges();
        }
    }
}
