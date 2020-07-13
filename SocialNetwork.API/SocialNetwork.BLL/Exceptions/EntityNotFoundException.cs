using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; }
        public string EntityId { get; }

        public EntityNotFoundException(Type entityType, string entityId) :
            base($"Entity {entityType.Name} with id {entityId} doesn't exist.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}
