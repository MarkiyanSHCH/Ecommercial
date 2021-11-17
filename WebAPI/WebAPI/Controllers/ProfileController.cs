using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Core.Services;
using WebAPI.Models;
using Domain.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileServices _profileServices;
        private readonly AuthServices _authServices;

        public ProfileController(ProfileServices profileServices, AuthServices authServices)
            => (this._profileServices, this._authServices) = (profileServices, authServices);

        [HttpGet]
        public IActionResult GetProfileById()
        {
            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized(new ProblemDetails { Title = "Failed to authenticate web user." });

            Account getAccount = this._profileServices.GetProfileById(userId);

            if (getAccount == null)
                return NotFound(new ProblemDetails { Title = "Web user was not found." });

            return Ok(new GetProfileResponse
            {
                Name = getAccount.Name,
                Email = getAccount.Email
            });
        }

        [HttpPatch("edit/name")]
        public IActionResult UpdateUserName([FromBody] PatchUserNameRequest patchUserName)
        {
            if (string.IsNullOrWhiteSpace(patchUserName.Name))
                return BadRequest(new ProblemDetails { Title = "Invalid request parameters." });

            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized(new ProblemDetails { Title = "Failed to authenticate web user." });

            if (!this._profileServices.UpdateName(userId, patchUserName.Name))
                return UnprocessableEntity(new ProblemDetails { Title = "Failed to update name." });

            return NoContent();
        }

        [HttpPatch("edit/password")]
        public IActionResult UpdateUserPassword([FromBody] PatchUserPasswordRequest patchUserPassword)
        {
            if (string.IsNullOrWhiteSpace(patchUserPassword.OldPassword) && string.IsNullOrWhiteSpace(patchUserPassword.NewPassword))
                return BadRequest(new ProblemDetails { Title = "Invalid request parameters." });

            int userId = this._authServices.GetUserId(base.User);
            if (userId <= 0)
                return Unauthorized(new ProblemDetails { Title = "Failed to authenticate web user." });

            Account user = this._profileServices.GetProfileById(userId);
            if (user == null)
                return NotFound(new ProblemDetails { Title = "Web user was not found." });

            if (!this._authServices.Authenticate(user, patchUserPassword.OldPassword))
                return UnprocessableEntity(new ProblemDetails { Title = "Current password is incorrect." });

            if (!this._profileServices.UpdatePassword(userId, patchUserPassword.NewPassword))
                return UnprocessableEntity(new ProblemDetails { Title = "Failed to update password." });

            return NoContent();
        }
    }
}
