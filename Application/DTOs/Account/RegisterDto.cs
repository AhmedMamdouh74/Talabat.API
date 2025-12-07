using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string? DisplayName { get; set; }
        [EmailAddress,Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
