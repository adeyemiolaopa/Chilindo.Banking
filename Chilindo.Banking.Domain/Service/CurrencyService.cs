using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Service
{
    public class CurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public Currency GetByID(string currencyID)
        {
            var currencyDto = _currencyRepository.FindByID(currencyID);
            var currency = AutoMapper.Mapper.Map<CurrencyDto, Currency>(currencyDto);
            return currency;
        }
    }
}
