using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Validators;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
    }
}
