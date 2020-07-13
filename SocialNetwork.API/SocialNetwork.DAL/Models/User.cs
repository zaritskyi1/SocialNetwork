using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.DAL.Models
{
    public class User : IdentityUser
    {
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

        public ICollection<Friendship> FriendshipsSent { get; set; }
        public ICollection<Friendship> FriendshipsReceived { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<MessageReport> MessageReports { get; set; }
    }
}
