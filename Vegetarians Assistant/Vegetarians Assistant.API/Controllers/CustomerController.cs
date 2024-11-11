using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.Membership;
using Vegetarians_Assistant.Services.Tool;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IMembershipTierService _membershipTierService;
        private readonly IUsermembershipService _usermembershipService;
        private readonly IConfiguration _configuration;
        private readonly ILoginCService _loginCService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerManagementService customerManagementService, IMembershipTierService membershipTierService, 
            IUsermembershipService usermembershipService, IConfiguration configuration, ILoginCService loginCService, IUnitOfWork unitOfWork)
        {
            _customerManagementService = customerManagementService;
            _membershipTierService = membershipTierService;
            _usermembershipService = usermembershipService;
            _configuration = configuration;
            _loginCService = loginCService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("/api/v1/customers/login")]
        public async Task<IActionResult> Login([FromBody] LoginView loginInfo)
        {
            JWT jwt = new(_configuration);
            var check = await _loginCService.Login(loginInfo);

            if (check == null)
            {
                return NotFound("No account found");
            }
            else if(check.Status.Equals("inactive"))
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
                    _ => "User" // Default role if none match
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
            //if (check.RoleId.Equals(1))
            //{
            //    return Ok(new { token = jwt.GenerateJwtToken(loginInfo.PhoneNumber, "Admin"), check.UserId});
            //}
            //else if(check.RoleId.Equals(2))
            //{
            //    return Ok(jwt.GenerateJwtToken(loginInfo.PhoneNumber, "Staff"));
            //}
            //else if (check.RoleId.Equals(3))
            //{
            //    return Ok(jwt.GenerateJwtToken(loginInfo.PhoneNumber, "Customer"));
            //}
            //else if(check.RoleId.Equals(4))
            //{
            //    return Ok(jwt.GenerateJwtToken(loginInfo.PhoneNumber, "Moderator"));
            //}
            //else if(check.RoleId.Equals(5))
            //{
            //    return Ok(jwt.GenerateJwtToken(loginInfo.PhoneNumber, "Nutritionist"));
            //}
            }
            return BadRequest("Login Failed");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/customers/getDeliveryInformationByUserId /{id}")]
        public async Task<ActionResult<DeliveryView>> GetDeliveryInformationByUserId(int id)
        {
            var deliveryDetail = await _customerManagementService.GetDeliveryInformationByUserId(id);
            if (deliveryDetail == null)
            {
                return NotFound("Deliverys not found");
            }
            return Ok(deliveryDetail);
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("/api/v1/customers/membership/{id}")]
        public async Task<IActionResult> GetCustomerMembership(int id)
        {
            try
            {
                var view = await _usermembershipService.getCustomerMembership(id);
                return Ok(view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("/api/v1/customers/membershipTier/{id}")]
        public async Task<IActionResult> GetCustomerMembershipTier(int id)
        {
            try
            {
                var view = await _membershipTierService.getCustomerMembershipTier(id);
                return Ok(view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/customers/RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserView newUser)
        {
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

        [Authorize(Roles = "Customer")]
        [HttpPut("/api/v1/customers/EditCustomer/membership/changePoint/{userId}/{points}")]
        public async Task<IActionResult> changePoint(int userId, int points)
        {
            try
            {
                var view = await _usermembershipService.addPointForMembership(userId, points);
                return Ok(view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [Authorize(Roles = "Customer")]
        [HttpPut("/api/v1/customers/EditCustomer")]
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

    }
}
