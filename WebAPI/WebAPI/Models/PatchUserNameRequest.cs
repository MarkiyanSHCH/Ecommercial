using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PatchUserNameRequest
    {
        [Required]
        [RegularExpression(@"[A-Za-zÀ-ÿ' -]+")]
        public string Name { get; set; }
    }
}
