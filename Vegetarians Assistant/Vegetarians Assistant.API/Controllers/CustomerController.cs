using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManagementService _customerManagementService;
        public CustomerController(ICustomerManagementService customerManagementService)
        {
            _customerManagementService = customerManagementService;
        }

        [HttpPost("/api/v1/customers/RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserView newUser)
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
            if (await _customerManagementService.IsExistedEmail(newUser.Email))
            {
                return BadRequest("Email you entered has already existed");
            }
            if (await _customerManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return BadRequest("Phone you entered has already existed");
            }
            var staff = await _customerManagementService.GetUserByUsername(newUser.Username);
            if (staff == null)
            {
                bool checkRegister = await _customerManagementService.CreateUserCustomer(newUser);
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
        
           [HttpPost("/api/v1/customers/EditCustomer")]
   public async Task<IActionResult> EditCustomer([FromBody] UserView newUser)
   {
       try
       {
           var view = await _customerManagementService.EditUser(newUser);
           return Ok(view);
       }
       catch (Exception ex)

       {
           return BadRequest("Chỉnh sửa thông tin khách hàng thất bại");
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
