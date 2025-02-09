using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO
{
    public class RegistrtDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress{ get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
      //  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        // ErrorMessage = "The password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }
    }
}
