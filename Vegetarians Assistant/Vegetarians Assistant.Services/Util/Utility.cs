using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;

namespace Vegetarians_Assistant.Services.Util
{
    public class Utility
    {
        private readonly ICustomerManagementService _customerManagementService;

        public Utility(ICustomerManagementService customerManagementService)
        {
            _customerManagementService = customerManagementService;
        }

        private static bool IsValidEmail(string email)
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

        public static ErrorView? validateCustomer(UserView newUser)
        {
            if (string.IsNullOrEmpty(newUser.Username))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "User name is required"
                };
            }
            if (newUser.Username.Length > 100)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "User name must be 100 characters or less"
                };
            }
            if (string.IsNullOrEmpty(newUser.Fullname))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Full name is required"
                };
            }
            if (newUser.Fullname.Length > 100)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Full name must be 100 characters or less"
                };
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Email is required"
                };
            }
            if (!IsValidEmail(newUser.Email))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Invalid email address"
                };
            }

            if (string.IsNullOrEmpty(newUser.Address))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Address is required"
                };
            }
            if (newUser.Address.Length > 100)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Address must be 100 characters or less"
                };
            }

            if (string.IsNullOrEmpty(newUser.PhoneNumber))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Phone number is required"
                };
            }
            if (!Regex.IsMatch(newUser.PhoneNumber, @"^[0-9]{10}$"))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Phone number must be exactly 10 digits"
                };
            }

            if (!newUser.Age.HasValue)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Age is required"
                };
            }
            if (newUser.Age < 0 || newUser.Age > 120)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Age must be between 0 and 120"
                };
            }

            if (string.IsNullOrEmpty(newUser.Gender))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Gender is required"
                };
            }
            if (newUser.Gender != "Men" && newUser.Gender != "Women")
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Gender must be either 'Men' or 'Women"
                };
            }

            if (!newUser.Age.HasValue)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Age is required"
                };
            }
            if (newUser.Age < 6 || newUser.Age > 120)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Age must be between 6 and 120"
                };
            }

            if (!newUser.Height.HasValue)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Height is required"
                };
            }
            if (newUser.Height < 80 || newUser.Height > 300)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Height must be between 80 and 300 centimeters"
                };
            }

            if (!newUser.Weight.HasValue)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Weight is required"
                };
            }

            if (newUser.Weight < 15 || newUser.Weight > 500)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Weight must be between 15 and 500 kilograms"
                };
            }

            if (string.IsNullOrEmpty(newUser.ActivityLevel))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Activity Level is required"
                };
            }
            if (newUser.ActivityLevel != "low" && newUser.ActivityLevel != "medium" && newUser.ActivityLevel != "high")
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Activity Level must be either 'low', 'medium', or 'high"
                };
            }

            if (!newUser.DietaryPreferenceId.HasValue)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "DietaryPreference is required"
                };
            }

            if (newUser.DietaryPreferenceId < 1 || newUser.DietaryPreferenceId > 5)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Dietary Preference ID must be between 1 and 5"
                };
            }

            if (string.IsNullOrEmpty(newUser.Profession))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Profession is required"
                };
            }
            if (newUser.Profession.Length > 100)
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Profession must be 100 characters or less"
                };
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Password is required"
                };
            }
            if (newUser.Password.Length < 10 || !Regex.IsMatch(newUser.Password, @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])"))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Password must be at least 10 characters long and contain at least one uppercase letter, one lowercase letter, and one number"
                };
            }
            /*if (await _customerManagementService.IsExistedEmail(newUser.Email))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Email you entered has already existed"
                };
            }
            if (await _customerManagementService.IsExistedPhone(newUser.PhoneNumber))
            {
                return new ErrorView
                {
                    status = 400,
                    message = "Phone you entered has already existed"
                };
            }*/

            return null;
        }
    }
}
