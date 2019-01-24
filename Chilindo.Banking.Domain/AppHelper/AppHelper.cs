using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.AppHelper
{
    public class AppHelper
    {
        public static decimal CalculateAmountWithRates(decimal amount, decimal accountExchangeRate, decimal transactionExchangeRate)
        {
            if (accountExchangeRate > transactionExchangeRate) {
                return (amount * accountExchangeRate) / transactionExchangeRate;
            }
            else {
                return (amount / transactionExchangeRate) * accountExchangeRate;
            }
        }
    }
}
