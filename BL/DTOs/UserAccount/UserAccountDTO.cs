using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.UserAccount
{
    public class UserAccountDTO
    {
        public Guid ID { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }


        public override string ToString()
        {
            return $"{this.Email}";
        }
    }
}
