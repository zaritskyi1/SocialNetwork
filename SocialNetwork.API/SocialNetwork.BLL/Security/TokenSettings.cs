namespace SocialNetwork.BLL.Security
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public long AccessTokenExpiration { get; set; }
    }
}
