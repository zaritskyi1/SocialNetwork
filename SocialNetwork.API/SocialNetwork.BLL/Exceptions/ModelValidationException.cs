using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class ModelValidationException : ArgumentException
    {
        public ModelValidationException(string message) 
            : base(message) { }
    }
}
