using System;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public string Activities { get; set; }
        public string FavoriteMovies { get; set; }
        public string FavoriteGames { get; set; }
        public string FavoriteQuotes { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
    }
}
