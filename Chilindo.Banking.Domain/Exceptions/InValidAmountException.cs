using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Exceptions
{
    public class InValidAmountException : Exception, ISerializable
    {
        public InValidAmountException() : base()
        {
        }

        public InValidAmountException(string message) : base(message)
        {
        }

        public InValidAmountException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InValidAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
