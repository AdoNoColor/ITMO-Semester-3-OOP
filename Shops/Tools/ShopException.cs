using System;
using System.Runtime.Serialization;

namespace Shops.Tools
{
    public class ShopException : Exception
    {
        public ShopException()
        {
        }

        public ShopException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }

        public ShopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ShopException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}