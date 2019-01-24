using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Exceptions
{
    public class InValidCurrencyIDException : Exception, ISerializable
    {
        public InValidCurrencyIDException() : base()
        {
        }

        public InValidCurrencyIDException(string message) : base(message)
        {
        }

        public InValidCurrencyIDException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InValidCurrencyIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
