namespace SocialNetwork.BLL.Exceptions
{
    public class InvalidUserIdException : ModelValidationException
    {
        public InvalidUserIdException(string paramName) : 
            base($"User id does not match.", paramName)
        {
        }
    }
}
