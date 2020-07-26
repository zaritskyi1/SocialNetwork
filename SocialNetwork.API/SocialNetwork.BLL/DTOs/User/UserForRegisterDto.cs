using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Validators;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
    }
}
