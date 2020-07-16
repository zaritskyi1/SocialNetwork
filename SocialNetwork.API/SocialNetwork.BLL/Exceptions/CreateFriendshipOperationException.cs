using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class CreateFriendshipOperationException : InvalidOperationException
    {
        public string Id { get; }

        public CreateFriendshipOperationException(string id, string message = "Can't create friendship request.") :
            base(message)
        {
            Id = id;
        }
    }
}
