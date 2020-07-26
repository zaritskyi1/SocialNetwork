using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class InvalidBusinessOperationException : InvalidOperationException
    {
        public InvalidBusinessOperationException(string message) : base(message) { }
    }
}
