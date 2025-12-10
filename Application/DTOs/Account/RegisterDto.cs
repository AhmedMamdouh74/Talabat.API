using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterDto
    {
        [Required]

        public string? DisplayName { get; set; }
        [EmailAddress,Required]
        public string? Email { get; set; }
        [Required,RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$",ErrorMessage = "Password must be at least 6 characters long, \r\nand include at least one uppercase letter, \r\none lowercase letter, one number, and one special character (@$!%*?&).\r\n")]

        public string? Password { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
    }
}
