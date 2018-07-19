using System;
using System.Runtime.Serialization;

namespace VigenereTools
{
    public class CipherException : Exception
    {
        public CipherException() : base() { }

        public CipherException(string message) : base(message) { }

        public CipherException(string message , Exception inner):base(message, inner) { }

        public CipherException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
