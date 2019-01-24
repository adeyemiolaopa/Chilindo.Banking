using Chilindo.Banking.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Persistence.Context
{
    public class BankingContext : DbContext
    {
        public BankingContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new BankingInitializer());
        }

        // For the Balance field
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                    .Property(A => A.Balance)
                    .HasColumnType("decimal").HasPrecision(15, 2);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
