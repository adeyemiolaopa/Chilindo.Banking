using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chilindo.Banking.Persistence.Repository;
using Chilindo.Banking.Domain.Service;
using Chilindo.Banking.Domain.Exceptions;
using System.Configuration;
using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Persistence.Context;

namespace Chilindo.Banking.Domain.Tests.Service
{
    [TestClass]
    public class CurrencyServiceTests
    {
        private string _connectionString;
        private UnitOfWork _unitOfWork;
        private CurrencyService _currencyService;

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ChilindoBankingConnString"].ConnectionString;
            _unitOfWork = new UnitOfWork(_connectionString);
            _currencyService = new CurrencyService(_unitOfWork._currencyRepository);

            // Auto Mapper Nugget to Map Classes
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<CurrencyDto, Currency>());
        }

        [TestCleanup]
        public void CleanUp()
        {
            AutoMapper.Mapper.Reset();
        }

        [TestMethod]
        public void GetByID_ValidCurrencyID_ReturnsCurrency()
        {
            // Arrange

            // Act
            var currency = _currencyService.GetByID("NGN");

            // Assert
            Assert.IsInstanceOfType(currency, typeof(Currency));
            Assert.AreEqual("NGN", currency.CurrencyID);
        }

        [TestMethod, ExpectedException(typeof(InValidCurrencyIDException))]
        public void GetByID_InValidCurrencyID_ThrowsException()
        {
            // Arrange

            // Act
            var currency = _currencyService.GetByID("AUD");

            // Assert
        }
    }
}
