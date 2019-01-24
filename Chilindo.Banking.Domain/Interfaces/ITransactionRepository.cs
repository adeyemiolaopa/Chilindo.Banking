using Chilindo.Banking.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.Banking.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDto Add(Guid TransactionID);
        //void UpdateSuccess(Transaction accountDetail);
        TransactionDto FindByID(Guid accountDetailID);
        List<TransactionDto> GetAll();

    }
}
