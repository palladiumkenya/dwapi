using System;

namespace Dwapi.UploadManagement.Core.Exceptions
{
    public class HandshakeException : Exception
    {
        public HandshakeException()
        {
        }

        public HandshakeException(string msg)
            : base(msg)
        {
        }
    }
}
