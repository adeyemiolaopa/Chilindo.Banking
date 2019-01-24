using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chilindo.Banking.Persistence.Repository;
using Chilindo.Banking.Domain.Service;
using System.Configuration;
using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Persistence.Context;

namespace Chilindo.Banking.Domain.Tests.Service
{
    [TestClass]
    public class BankAccountServiceTests
    {
        private string _connectionString;
        private UnitOfWork _unitOfWork;
        private BankAccountService _bankAccountService;

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ChilindoBankingConnString"].ConnectionString;
            _unitOfWork = new UnitOfWork(_connectionString);
            _bankAccountService = new BankAccountService(_unitOfWork._bankAccountRepository,
                                                           _unitOfWork._currencyRepository);

            // Auto Mapper Nugget to Map Classes
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<BankAccountDto, BankAccount>()
                                        //.ForMember(C => C.Currency, opt => opt.Ignore())
                                        .ForMember(A => A.Transactions, opt => opt.Ignore()));
        }

        [TestCleanup]
        public void CleanUp()
        {
            AutoMapper.Mapper.Reset();
        }

        [TestMethod]
        public void GetByID_ValidAccountNo_ReturnsAccount()
        {
            // Arrange

            // Act
            var account = _bankAccountService.GetByID(1277785430);

            // Assert
            Assert.IsInstanceOfType(account, typeof(BankAccount));
            Assert.AreEqual(1277785430, account.AccountNo);
        }

        [TestMethod, ExpectedException(typeof(InValidAccountNoException))]
        public void GetByID_InValidAccountNo_ThrowsException()
        {
            // Arrange

            // Act
            var account = _bankAccountService.GetByID(1277785439);

            // Assert
        }
    }
}
