using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Implement;
using Vegetarians_Assistant.Repo.Entity;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("/api/v1/users/alluser")]
        public async Task<ActionResult<IEnumerable<UserView>>> GetUsers()
        {
            var usersList = await _userManagementService.GetAllUser();
            if (usersList == null || !usersList.Any())
            {
                return NotFound("No users found on the system");
            }
            return Ok(usersList);
        }

        [Authorize(Roles = "Admin, Customer")]
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

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("/api/v1/users/getUserByID/{id}")]
        public async Task<ActionResult<UserView>> GetUserByID(int id)
        {
            var userDetail = await _userManagementService.GetUserByUserId(id);
            if (userDetail == null)
            {
                return NotFound("User not found");
            }
            return Ok(userDetail);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/v1/users/createStaff")]
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
