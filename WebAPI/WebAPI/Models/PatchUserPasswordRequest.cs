using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
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