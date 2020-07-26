using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class EntityExistsException : Exception
    {
        private const string DefaultMessage = "{0} already exists.";

        public Type EntityType { get; }

        public EntityExistsException(Type entityType) 
            : base(String.Format(DefaultMessage, entityType.Name))
        {
            EntityType = entityType;
        }
    }
}
