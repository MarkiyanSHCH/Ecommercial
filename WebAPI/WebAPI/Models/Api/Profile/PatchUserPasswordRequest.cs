using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Api.Profile
{
    public class PatchUserPasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}