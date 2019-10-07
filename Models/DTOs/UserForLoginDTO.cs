using System.ComponentModel.DataAnnotations;

namespace BoardGameDating.api.Models.DTOs
{
    public class UserForLoginDTO
    {
        [Required]
        [MaxLength(25), MinLength(4)]
        public string Name { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Please enter a password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}