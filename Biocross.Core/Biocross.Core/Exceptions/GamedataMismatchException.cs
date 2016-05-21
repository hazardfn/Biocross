using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Biocross.Core.Exceptions
{
    [Serializable]
    public class GamedataMismatchException : Exception
    {
        public GamedataMismatchException()
        {
        }

        public GamedataMismatchException(string message) : base(message)
        {
        }

        public GamedataMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GamedataMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
