using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Validators;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForUpdateDto
    {
        [StringLength(32)]
        public string Gender { get; set; }

        [StringLength(32)]
        public string City { get; set; }

        [StringLength(32)]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(256)]
        public string Status { get; set; }

        [StringLength(256)]
        public string Activities { get; set; }

        [StringLength(256)]
        public string FavoriteMovies { get; set; }

        [StringLength(256)]
        public string FavoriteGames { get; set; }

        [StringLength(256)]
        public string FavoriteQuotes { get; set; }
    }
}
