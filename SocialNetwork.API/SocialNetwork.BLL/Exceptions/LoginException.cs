using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class LoginException : EntityNotFoundException
    {
        private const string DefaultMessage = "Invalid login or password.";

        public LoginException(Type entityType) : base(DefaultMessage, entityType) { }
    }
}
