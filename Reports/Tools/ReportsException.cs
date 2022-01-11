using System;
using System.Runtime.Serialization;

namespace Reports.Tools
{
    public class ReportsException : Exception
    {
        public ReportsException()
        {
        }

        public ReportsException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }

        public ReportsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ReportsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}