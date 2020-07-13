using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public Type EntityType { get; }
        public string EntityId { get; }
        public string ControllerToRedirect { get; }
        public string MethodToRedirect { get; }

        public EntityAlreadyExistsException(
            Type entityType, string entityId, string controllerToRedirect, string methodToRedirect) 
            : base($"{entityType.Name} exists with id - {entityId}")
        {
            EntityType = entityType;
            EntityId = entityId;
            ControllerToRedirect = controllerToRedirect;
            MethodToRedirect = methodToRedirect;
        }
    }
}
