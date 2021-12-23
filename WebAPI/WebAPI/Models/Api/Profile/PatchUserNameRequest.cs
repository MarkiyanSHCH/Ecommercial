using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Api.Profile
{
    public class PatchUserNameRequest
    {
        [Required]
        [RegularExpression(@"[A-Za-zÀ-ÿ' -]+")]
        public string Name { get; set; }
    }
}
