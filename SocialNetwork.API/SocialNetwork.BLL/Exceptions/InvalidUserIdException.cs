using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class InvalidUserIdException : ModelValidationException
    {
        private const string DefaultMessage = "User id in {0} does not match with current user id.";

        public InvalidUserIdException(Type entityType) : 
            base(String.Format(DefaultMessage, entityType.Name)) { }
    }
}
