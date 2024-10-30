using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoginAService _loginAService;
        private readonly IUserManagementService _userManagementService;
        public UserController(ILoginAService loginAService, IUserManagementService userManagementService)
        {
            _loginAService = loginAService;
            _userManagementService = userManagementService;
        }
        [HttpPost("/api/v1/users/login")]
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

            var user_ = await _loginAService.AuthenticateUser(loginInfo);
            if (user_ == null)
            {
                return NotFound("No account found");
            }
            else if (user_.Status.Equals("banned"))
            {
                return BadRequest("Your account is banned");
            }
            else
            {
                return Ok(user_);
            }
        }



        [HttpGet("/api/v1/users/alluser")]
        public async Task<ActionResult<IEnumerable<UserView>>> GetUsers()
        {

            var usersList = await _userManagementService.GetAllUser();
            if (usersList.IsNullOrEmpty())
            {
                return NotFound("No users found on the system");
            }
            return Ok(usersList);
        }



        [HttpGet("/api/v1/users/getUserByUsername/{username}")]
        public async Task<ActionResult<UserView>> GetUserByUserName(string username)
        {
            var userDetail = await _userManagementService.GetUserByUsername(username);
            if (userDetail == null)
            {
                return NotFound("User not found");
            }
            return Ok(userDetail);
        }


        [HttpGet("/api/v1/users/GetUserByID/{id}")]
        public async Task<ActionResult<UserView>> GetUserByID(int id)
        {
            var userDetail = await _userManagementService.GetUserByUserId(id);
            if (userDetail == null)
            {
                return NotFound("Users not found");
            }
            return Ok(userDetail);
        }






        [HttpPost("/api/v1/users/CreateStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] StaffView newUser)
        {
            if (await _userManagementService.IsExistedEmail(newUser.Email))
            {
                return BadRequest("Email you entered has already existed");
            }
            if (await _userManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return BadRequest("Phone you entered has already existed");
            }
            if (await _userManagementService.IsExistedUserName(newUser.Username))
            {
                return BadRequest("Username you entered has already existed");
            }
                bool checkRegister = await _userManagementService.CreateUserStaff(newUser);
                if (checkRegister)
                {
                    return Ok("Create success");
                }
                else
                {
                    return BadRequest("Not correct role");
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
