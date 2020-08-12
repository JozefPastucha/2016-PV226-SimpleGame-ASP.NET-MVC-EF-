using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.UserAccount
{
    /// <summary>
    /// Wrapper for user login details
    /// </summary>
    public class UserLoginDTO
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}