using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Exceptions
{
    public class InsufficientBalanceException : Exception, ISerializable
    {
        public InsufficientBalanceException() : base()
        {
        }

        public InsufficientBalanceException(string message) : base(message)
        {
        }

        public InsufficientBalanceException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InsufficientBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
