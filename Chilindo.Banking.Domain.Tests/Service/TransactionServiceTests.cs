using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chilindo.Banking.Domain.Exceptions;
using Chilindo.Banking.Domain.Dto;
using Chilindo.Banking.Domain.Service;
using System.Configuration;
using Chilindo.Banking.Persistence.Repository;
using Chilindo.Banking.Persistence.Context;

namespace Chilindo.Banking.Domain.Tests.Service
{
    [TestClass]
    public class TransactionServiceTests
    {
        private string _connectionString;
        private UnitOfWork _unitOfWork;
        private BankAccountService _bankAccountService;
        private TransactionService _transactionService;

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ChilindoBankingConnString"].ConnectionString;
            _unitOfWork = new UnitOfWork(_connectionString);
            _bankAccountService = new BankAccountService(_unitOfWork._bankAccountRepository, 
                                                         _unitOfWork._currencyRepository);

            _transactionService = new TransactionService(_unitOfWork._bankAccountRepository, 
                                                         _unitOfWork._transactionRepository, 
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
        public void CreateDeposit_ValidAmount_ReturnsAccountDetail()
        {
            // Arrange

            // Act
            var accountDetail = _transactionService.CreateDeposit(1277785430, 2500, "NGN");
            var account = _bankAccountService.GetByID(1277785430);

            // Assert
            Assert.IsInstanceOfType(accountDetail, typeof(Transaction));
            Assert.AreEqual(500, accountDetail.Amount);
            //Assert.AreEqual(true, accountDetail.Success);
            Assert.IsTrue(account.Balance >= 500);
        }

        [TestMethod]
        public void CreateWithdrawal_ValidAmount_ReturnsAccountDetail()
        {
            // Arrange

            // Act
            var account = _bankAccountService.GetByID(1277785430);
            var accountDetail = _transactionService.CreateWithdrawal(1277785430, 200, "NGN");
            var newAccount = _bankAccountService.GetByID(1277785430);

            // Assert
            Assert.IsInstanceOfType(accountDetail, typeof(Transaction));
            Assert.AreEqual(200, accountDetail.Amount);
            //Assert.AreEqual(true, accountDetail.Success);
            Assert.IsTrue(account.Balance > newAccount.Balance);
        }

        [TestMethod, ExpectedException(typeof(InValidAmountException))]
        public void CreateDeposit_InValidAmount_ThrowsException()
        {
            // Arrange

            // Act
            var accountDetail = _transactionService.CreateDeposit(1277785430, -200, "NGN");

            // Assert
        }

        [TestMethod, ExpectedException(typeof(InValidAmountException))]
        public void CreateWithdrawal_InValidAmount_ThrowsException()
        {
            // Arrange

            // Act
            var accountDetail = _transactionService.CreateWithdrawal(1277785430, -200, "NGN");

            // Assert
        }

        [TestMethod, ExpectedException(typeof(InsufficientBalanceException))]
        public void CreateWithdrawal_ExcessAmount_ThrowsException()
        {
            // Arrange

            // Act
            var accountDetail = _transactionService.CreateWithdrawal(1277785430, 225000, "NGN");

            // Assert
        }
    }
}
