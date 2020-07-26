using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class AccessDeniedException : InvalidOperationException
    {
        private const string DefaultMessage = "You do not have access to {0}.";

        public string ResourceName { get; }

        public AccessDeniedException(string resourceName) 
            : base(String.Format(DefaultMessage, resourceName))
        {
            ResourceName = resourceName;
        }

        public AccessDeniedException(Type resourceType) 
            : base(String.Format(DefaultMessage, resourceType.Name))
        {
            ResourceName = resourceType.Name;
        }
    }
}
