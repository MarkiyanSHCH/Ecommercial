using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PostRegistrationAccountRequest
    {
        [Required]
        [RegularExpression(@"^[A-Za-zÀ-ÿ -]+$")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[A-Za-zÀ-ÿ0-9.@]+$")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
