using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Validators;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
    }
}
