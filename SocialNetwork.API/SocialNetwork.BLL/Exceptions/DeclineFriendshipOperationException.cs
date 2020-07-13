using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class DeclineFriendshipOperationException : InvalidOperationException
    {
        public string Id { get; }

        public DeclineFriendshipOperationException(string id, string message = "Can't decline friendship request.") :
            base(message)
        {
            Id = id;
        }
    }
}
