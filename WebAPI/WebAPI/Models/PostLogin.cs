using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PostLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}