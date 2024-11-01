using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.Membership;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IMembershipTierService _membershipTierService;
        private readonly IUsermembershipService _usermembershipService;
        public CustomerController(ICustomerManagementService customerManagementService, IMembershipTierService membershipTierService, IUsermembershipService usermembershipService)
        {
            _customerManagementService = customerManagementService;
            _membershipTierService = membershipTierService;
            _usermembershipService = usermembershipService;
        
    }

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
    }
}
