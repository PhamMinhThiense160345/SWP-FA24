using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;

namespace Vegetarians_Assistant.Services.Services.Implement.Customer
{
    public class CustomerManagementService : ICustomerManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> IsExistedEmail(string email)
        {
            try
            {
                bool status = true;
                var existed = (await _unitOfWork.UserRepository.FindAsync(e => e.Email == email)).FirstOrDefault();
                if (existed == null)
                {
                    status = false;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> IsExistedPhone(string phone)
        {
            try
            {
                bool status = true;
                var existed = (await _unitOfWork.UserRepository.FindAsync(e => e.PhoneNumber == phone)).FirstOrDefault();
                if (existed == null)
                {
                    status = false;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<UserView?> GetUserByUsername(String username)
        {

            try
            {
                var user = (await _unitOfWork.UserRepository.FindAsync(c => c.Username == username)).FirstOrDefault();
                if (user != null)
                {
                    var userView = new UserView()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Username = user.Username,
                        Weight = user.Weight,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        Gender = user.Gender,
                        Height = user.Height,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession
                    };
                    return userView;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateUserCustomer(UserView newUser)
        {
            try
            {
                bool status = false;
                newUser.Status = "active";
                newUser.RoleId = 3;
                var user = _mapper.Map<User>(newUser);
                await _unitOfWork.UserRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    if (newUser.RoleId == 3)
                    {
                        var staff = new User
                        {
                            Username = insertedUser.Username,
                            Email = insertedUser.Email,
                            Address = insertedUser.Address,
                            PhoneNumber = insertedUser.PhoneNumber,
                            Age = insertedUser.Age,
                            Gender = insertedUser.Gender,
                            Height = insertedUser.Height,
                            Weight = insertedUser.Weight,
                            ActivityLevel = insertedUser.ActivityLevel,
                            DietaryPreferenceId = insertedUser.DietaryPreferenceId,
                            Profession = insertedUser.Profession,
                            Password = insertedUser.Password
                        };
                        await _unitOfWork.SaveAsync();
                        status = true;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    await _unitOfWork.UserRepository.DeleteAsync(insertedUser);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }
         public async Task<UserView?> EditUser(UserView view)
 {
     var user = _mapper.Map<User>(view);
     if (user != null)
     {
         await _unitOfWork.UserRepository.UpdateAsync(user);
         await _unitOfWork.SaveAsync();
         return view;
     }

     return null;
 }
    }
}
