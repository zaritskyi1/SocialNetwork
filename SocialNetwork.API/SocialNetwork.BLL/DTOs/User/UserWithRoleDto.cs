using System.Collections.Generic;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserWithRoleDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IList<string> Roles { get; set; }
    }
}
