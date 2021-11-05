using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PostLoginRequest
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[A-Za-zÀ-ÿ0-9.@]+$")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}