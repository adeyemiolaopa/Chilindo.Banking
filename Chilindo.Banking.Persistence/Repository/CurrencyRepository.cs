using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chilindo.Banking.Domain;
using Chilindo.Banking.Persistence.Context;
using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;

namespace Chilindo.Banking.Persistence.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly BankingContext _db;

        public CurrencyRepository(BankingContext context)
        {
            _db = context;
        }

        public void Add(Currency currency)
        {
            if (currency == null) {
                throw new Exception();
            }

            _db.Currencies.Add(currency);
            _db.SaveChanges();
        }

        public CurrencyDto FindByID(string currencyID)
        {
            var currency = _db.Currencies.Find(currencyID);

            if (currency == null) {
                throw new InValidCurrencyIDException("Your currency is not in correct format. Pass your currencyID!");
            }

            var currencyDto = AutoMapper.Mapper.Map<Currency, CurrencyDto>(currency);
            return currencyDto;
        }

        public List<CurrencyDto> GetAll()
        {
            var currencies = _db.Currencies.OrderBy(O => O.CurrencyID).ToList();
            var newcurrencies = AutoMapper.Mapper.Map<List<Currency>, List<CurrencyDto>>(currencies);
            return newcurrencies;
        }
    }
}
