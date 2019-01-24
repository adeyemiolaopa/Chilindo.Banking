using Chilindo.Banking.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilindo.Banking.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        BankAccountDto Add(BankAccount account);
        BankAccount FindByID(int accountNo);
        List<BankAccountDto> GetAll();
    }
}
