using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private const string DefaultMessage = "{0} with id - {1} doesn't exist.";

        public Type EntityType { get; }

        public EntityNotFoundException(string message, Type entityType) : base(message)
        {
            EntityType = entityType;
        }

        public EntityNotFoundException(Type entityType, string entityId) :
            base(String.Format(DefaultMessage, entityType.Name, entityId))
        {
            EntityType = entityType;
        }

    }
}
