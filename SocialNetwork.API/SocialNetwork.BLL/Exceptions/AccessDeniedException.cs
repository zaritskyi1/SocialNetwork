using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class AccessDeniedException : InvalidOperationException
    {
        public Type EntityType { get; }

        public AccessDeniedException(Type entityType) 
            : base($"You do not have access to this {entityType.Name}")
        {
            EntityType = entityType;
        }
    }
}
