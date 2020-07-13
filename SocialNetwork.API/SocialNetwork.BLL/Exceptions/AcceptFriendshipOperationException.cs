using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class AcceptFriendshipOperationException : InvalidOperationException
    {
        public string Id { get; }

        public AcceptFriendshipOperationException(string id, string message = "Can't accept friendship request.") :
            base(message)
        {
            Id = id;
        }
    }
}
