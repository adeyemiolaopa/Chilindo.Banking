using Chilindo.Banking.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.Banking.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        void Add(Currency currency);
        CurrencyDto FindByID(string currencyID);
        List<CurrencyDto> GetAll();
    }
}
