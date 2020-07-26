using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class AcceptFriendshipOperationException : InvalidBusinessOperationException
    {
        private const string DefaultMessage = "Can't accept friendship request with id - {0}. It's not pending.";

        public string FriendshipId { get; }

        public AcceptFriendshipOperationException(string friendshipId) 
            : base(String.Format(DefaultMessage, friendshipId))
        {
            FriendshipId = friendshipId;
        }
    }
}
