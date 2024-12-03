using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Implement;
using Vegetarians_Assistant.Repo.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Vegetarians_Assistant.Services.Tool;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoginAService _loginAService;
        private readonly IUserManagementService _userManagementService;
        private readonly IConfiguration _configuration;

        public UserController(ILoginAService loginAService, IUserManagementService userManagementService, IConfiguration configuration)
        {
            _loginAService = loginAService;
            _userManagementService = userManagementService;
            _configuration = configuration;
        }

        [HttpPost("/api/v1/users/login")]
        public async Task<IActionResult> Login([FromBody] LoginView loginInfo)
        {
            JWT jwt = new(_configuration);
            var check = await _loginAService.Login(loginInfo);

            if (check == null)
            {
                return NotFound("Phone number does not exist");
            }
            else if (check.Status.Equals("inactive"))
            {
                return BadRequest("Your account is baned");
            }
            else if (check != null)
            {
                string role = check.RoleId switch
                {
                    1 => "Admin",
                    2 => "Staff",
                    3 => "Customer",
                    4 => "Moderator",
                    5 => "Nutritionist",
                    _ => "User" 
                };
                string token = jwt.GenerateJwtToken(loginInfo.PhoneNumber, role);
                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        check.UserId,
                        check.Username,
                        check.Password,
                        check.PhoneNumber,
                        check.Email,
                        check.Address,
                        check.RoleId,
                        check.Status,
                        check.Gender,
                        check.DietaryPreferenceId,
                        check.Goal,
                        check.ActivityLevel,
                        check.Age,
                        check.ImageUrl,
                        check.Height,
                        check.Weight,
                        check.Profession,
                        check.IsPhoneVerified
                    }
                });
            }
            return BadRequest("Login Failed");
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
