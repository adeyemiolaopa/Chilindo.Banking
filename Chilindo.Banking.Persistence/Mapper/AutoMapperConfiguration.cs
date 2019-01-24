using Chilindo.Banking.Domain;
using Chilindo.Banking.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Persistence.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<BankAccountDto, BankAccount>()
                                        //.ForMember(C => C.Currency, opt => opt.Ignore())
                                        .ForMember(A => A.Transactions, opt => opt.Ignore()));
        }
    }
}
