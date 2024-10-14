using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoginAService _loginAService;
        public UserController(ILoginAService loginAService)
        {
            _loginAService = loginAService;
        }
        [HttpPost("/api/v1/users/signin")]
        public async Task<IActionResult> Login([FromBody] LoginView loginInfo)
        {
            if (loginInfo.Email.IsNullOrEmpty())
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(loginInfo.Email))
            {
                return BadRequest("Invalid email address");
            }
            else if (loginInfo.Password.IsNullOrEmpty())
            {
                return BadRequest("Password is required");
            }

            var account_ = await _loginAService.AuthenticateUser(loginInfo);
            if (account_ == null)
            {
                return NotFound("No account found");
            }
            else if (account_.Status.Equals("banned"))
            {
                return BadRequest("Your account is banned");
            }
            else
            {
                return Ok(account_);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
