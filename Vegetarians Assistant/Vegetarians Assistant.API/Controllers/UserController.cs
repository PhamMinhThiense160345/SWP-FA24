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
            return userDetail;
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






        [HttpPost("/api/v1/users/RegisterStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] UserView newUser)
        {
            if (string.IsNullOrEmpty(newUser.Username))
            {
                return BadRequest("User name is required");
            }
            if (newUser.Username.Length > 100)
            {
                return BadRequest("User name must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(newUser.Email))
            {
                return BadRequest("Invalid email address");
            }

            if (string.IsNullOrEmpty(newUser.Address))
            {
                return BadRequest("Address is required");
            }
            if (newUser.Address.Length > 100)
            {
                return BadRequest("Address must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.PhoneNumber))
            {
                return BadRequest("Phone number is required");
            }
            if (!Regex.IsMatch(newUser.PhoneNumber, @"^[0-9]{10}$"))
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }

            if (!newUser.Age.HasValue)
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 0 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 0 and 120");
            }

            if (string.IsNullOrEmpty(newUser.Gender))
            {
                return BadRequest("Gender is required");
            }
            if (newUser.Gender != "Men" && newUser.Gender != "Women")
            {
                return BadRequest("Gender must be either 'Men' or 'Women'");
            }

            if (!newUser.Age.HasValue)  
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 6 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 6 and 120");
            }

            if (!newUser.Height.HasValue)
            {
                return BadRequest("Height is required");
            }
            if (newUser.Height < 80 || newUser.Height > 300)
            {
                return BadRequest("Height must be between 80 and 300 centimeters");
            }

            if (!newUser.Weight.HasValue)
            {
                return BadRequest("Weight is required");
            }

            if (newUser.Weight < 15 || newUser.Weight > 500) 
            {
                return BadRequest("Weight must be between 15 and 500 kilograms");
            }

            if (string.IsNullOrEmpty(newUser.ActivityLevel))
            {
                return BadRequest("Activity Level is required");
            }
            if (newUser.ActivityLevel != "low" && newUser.ActivityLevel != "medium" && newUser.ActivityLevel != "high")
            {
                return BadRequest("Activity Level must be either 'low', 'medium', or 'high'");
            }

            if (!newUser.DietaryPreferenceId.HasValue)
            {
                return BadRequest("DietaryPreference is required");
            }

            if (newUser.DietaryPreferenceId < 1 || newUser.DietaryPreferenceId > 5)
            {
                return BadRequest("Dietary Preference ID must be between 1 and 5");
            }

            if (string.IsNullOrEmpty(newUser.Profession))
            {
                return BadRequest("Profession is required");
            }
            if (newUser.Profession.Length > 100)
            {
                return BadRequest("Profession must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Password is required");
            }
            if (newUser.Password.Length < 10 || !Regex.IsMatch(newUser.Password, @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])"))
            {
                return BadRequest("Password must be at least 10 characters long and contain at least one uppercase letter, one lowercase letter, and one number");
            }
            if (await _userManagementService.IsExistedEmail(newUser.Email))
            {
                return BadRequest("Email you entered has already existed");
            }
            if (await _userManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return BadRequest("Phone you entered has already existed");
            }
            var staff = await _userManagementService.GetUserByUsername(newUser.Username);
            if (staff == null)
            {
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
            else
            {
                return BadRequest("Existed username");
            }
        }





        [HttpPost("/api/v1/users/RegisterNutritionist")]
        public async Task<IActionResult> RegisterNutritionist([FromBody] UserView newUser)
        {
            if (string.IsNullOrEmpty(newUser.Username))
            {
                return BadRequest("User name is required");
            }
            if (newUser.Username.Length > 100)
            {
                return BadRequest("User name must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(newUser.Email))
            {
                return BadRequest("Invalid email address");
            }

            if (string.IsNullOrEmpty(newUser.Address))
            {
                return BadRequest("Address is required");
            }
            if (newUser.Address.Length > 100)
            {
                return BadRequest("Address must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.PhoneNumber))
            {
                return BadRequest("Phone number is required");
            }
            if (!Regex.IsMatch(newUser.PhoneNumber, @"^[0-9]{10}$"))
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }

            if (!newUser.Age.HasValue)
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 0 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 0 and 120");
            }

            if (string.IsNullOrEmpty(newUser.Gender))
            {
                return BadRequest("Gender is required");
            }
            if (newUser.Gender != "Men" && newUser.Gender != "Women")
            {
                return BadRequest("Gender must be either 'Men' or 'Women'");
            }

            if (!newUser.Age.HasValue)
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 6 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 6 and 120");
            }

            if (!newUser.Height.HasValue)
            {
                return BadRequest("Height is required");
            }
            if (newUser.Height < 80 || newUser.Height > 300)
            {
                return BadRequest("Height must be between 80 and 300 centimeters");
            }

            if (!newUser.Weight.HasValue)
            {
                return BadRequest("Weight is required");
            }

            if (newUser.Weight < 15 || newUser.Weight > 500)
            {
                return BadRequest("Weight must be between 15 and 500 kilograms");
            }

            if (string.IsNullOrEmpty(newUser.ActivityLevel))
            {
                return BadRequest("Activity Level is required");
            }
            if (newUser.ActivityLevel != "low" && newUser.ActivityLevel != "medium" && newUser.ActivityLevel != "high")
            {
                return BadRequest("Activity Level must be either 'low', 'medium', or 'high'");
            }

            if (!newUser.DietaryPreferenceId.HasValue)
            {
                return BadRequest("DietaryPreference is required");
            }

            if (newUser.DietaryPreferenceId < 1 || newUser.DietaryPreferenceId > 5)
            {
                return BadRequest("Dietary Preference ID must be between 1 and 5");
            }

            if (string.IsNullOrEmpty(newUser.Profession))
            {
                return BadRequest("Profession is required");
            }
            if (newUser.Profession.Length > 100)
            {
                return BadRequest("Profession must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Password is required");
            }
            if (newUser.Password.Length < 10 || !Regex.IsMatch(newUser.Password, @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])"))
            {
                return BadRequest("Password must be at least 10 characters long and contain at least one uppercase letter, one lowercase letter, and one number");
            }
            if (await _userManagementService.IsExistedEmail(newUser.Email))
            {
                return BadRequest("Email you entered has already existed");
            }
            if (await _userManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return BadRequest("Phone you entered has already existed");
            }
            var staff = await _userManagementService.GetUserByUsername(newUser.Username);
            if (staff == null)
            {
                bool checkRegister = await _userManagementService.CreateUserNutritionist(newUser);
                if (checkRegister)
                {
                    return Ok("Create success");
                }
                else
                {
                    return BadRequest("Not correct role");
                }
            }
            else
            {
                return BadRequest("Existed username");
            }
        }





        [HttpPost("/api/v1/users/RegisterModerator")]
        public async Task<IActionResult> RegisterModerator([FromBody] UserView newUser)
        {
            if (string.IsNullOrEmpty(newUser.Username))
            {
                return BadRequest("User name is required");
            }
            if (newUser.Username.Length > 100)
            {
                return BadRequest("User name must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(newUser.Email))
            {
                return BadRequest("Invalid email address");
            }

            if (string.IsNullOrEmpty(newUser.Address))
            {
                return BadRequest("Address is required");
            }
            if (newUser.Address.Length > 100)
            {
                return BadRequest("Address must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.PhoneNumber))
            {
                return BadRequest("Phone number is required");
            }
            if (!Regex.IsMatch(newUser.PhoneNumber, @"^[0-9]{10}$"))
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }

            if (!newUser.Age.HasValue)
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 0 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 0 and 120");
            }

            if (string.IsNullOrEmpty(newUser.Gender))
            {
                return BadRequest("Gender is required");
            }
            if (newUser.Gender != "Men" && newUser.Gender != "Women")
            {
                return BadRequest("Gender must be either 'Men' or 'Women'");
            }

            if (!newUser.Age.HasValue)
            {
                return BadRequest("Age is required");
            }
            if (newUser.Age < 6 || newUser.Age > 120)
            {
                return BadRequest("Age must be between 6 and 120");
            }

            if (!newUser.Height.HasValue)
            {
                return BadRequest("Height is required");
            }
            if (newUser.Height < 80 || newUser.Height > 300)
            {
                return BadRequest("Height must be between 80 and 300 centimeters");
            }

            if (!newUser.Weight.HasValue)
            {
                return BadRequest("Weight is required");
            }

            if (newUser.Weight < 15 || newUser.Weight > 500)
            {
                return BadRequest("Weight must be between 15 and 500 kilograms");
            }

            if (string.IsNullOrEmpty(newUser.ActivityLevel))
            {
                return BadRequest("Activity Level is required");
            }
            if (newUser.ActivityLevel != "low" && newUser.ActivityLevel != "medium" && newUser.ActivityLevel != "high")
            {
                return BadRequest("Activity Level must be either 'low', 'medium', or 'high'");
            }

            if (!newUser.DietaryPreferenceId.HasValue)
            {
                return BadRequest("DietaryPreference is required");
            }

            if (newUser.DietaryPreferenceId < 1 || newUser.DietaryPreferenceId > 5)
            {
                return BadRequest("Dietary Preference ID must be between 1 and 5");
            }

            if (string.IsNullOrEmpty(newUser.Profession))
            {
                return BadRequest("Profession is required");
            }
            if (newUser.Profession.Length > 100)
            {
                return BadRequest("Profession must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Password is required");
            }
            if (newUser.Password.Length < 10 || !Regex.IsMatch(newUser.Password, @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])"))
            {
                return BadRequest("Password must be at least 10 characters long and contain at least one uppercase letter, one lowercase letter, and one number");
            }
            if (await _userManagementService.IsExistedEmail(newUser.Email))
            {
                return BadRequest("Email you entered has already existed");
            }
            if (await _userManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return BadRequest("Phone you entered has already existed");
            }
            var staff = await _userManagementService.GetUserByUsername(newUser.Username);
            if (staff == null)
            {
                bool checkRegister = await _userManagementService.CreateUserModerator(newUser);
                if (checkRegister)
                {
                    return Ok("Create success");
                }
                else
                {
                    return BadRequest("Not correct role");
                }
            }
            else
            {
                return BadRequest("Existed username");
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
