using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.UserAccount
{
    /// <summary>
    /// Wrapper for user registration details
    /// </summary>
    public class UserRegistrationDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(36)]
        public string UserName { get; set; }

        public override string ToString()
        {
            return $"{this.Email}";
        }
    }
}
