using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo.Banking.Domain.Exceptions
{
    public class InValidAccountNoException : Exception, ISerializable
    {
        public InValidAccountNoException() : base()
        {
        }

        public InValidAccountNoException(string message) : base(message)
        {
        }

        public InValidAccountNoException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InValidAccountNoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
