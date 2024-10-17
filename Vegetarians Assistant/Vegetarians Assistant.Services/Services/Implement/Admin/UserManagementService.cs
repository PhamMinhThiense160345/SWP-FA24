using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.Services.Services.Implement.Admin
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<UserView>> GetAllUser()
        {
            try
            {
                var users = (await _unitOfWork.UserRepository.GetAsync()).ToList();
                List<UserView> userViews = new List<UserView>();
                foreach (User user in users)
                {
                    var userView = new UserView()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        DietaryPreferenceId = user.DietaryPreferenceId,
                        Fullname = user.Fullname,
                        Gender = user.Gender,
                        Height = user.Height,
                        ImageUrl = user.ImageUrl,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession,
                        RoleId = user.RoleId,
                        Status = user.Status,
                        Username = user.Username,
                        Weight = user.Weight
                    };
                    userViews.Add(userView);
                }
                return userViews;

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
                        Status = user.Status,
                        Username= user.Username,
                        Weight = user.Weight,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        DietaryPreferenceId = user.DietaryPreferenceId,
                        Fullname = user.Fullname,
                        Gender = user.Gender,
                        Height = user.Height,
                        ImageUrl = user.ImageUrl,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession,
                        RoleId = user.RoleId
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
        public async Task<UserView?> GetUserByUserId(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIDAsync(id);
                if (user != null)
                {
                    var userView = new UserView()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Username = user.Username,
                        Fullname= user.Fullname,
                        Status = user.Status,
                        Weight = user.Weight,
                        ActivityLevel= user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        DietaryPreferenceId= user.DietaryPreferenceId,
                        Gender= user.Gender,
                        Height = user.Height,
                        ImageUrl= user.ImageUrl,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession,
                        RoleId = user.RoleId
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
        public async Task<bool> CreateUserStaff(UserView newUser)
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
                            Fullname = newUser.Fullname,
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
                        await _unitOfWork.UserRepository.InsertAsync(staff);
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
        public async Task<bool> CreateUserNutritionist(UserView newUser)
        {
            try
            {
                bool status = false;
                newUser.Status = "active";
                newUser.RoleId = 6;
                var user = _mapper.Map<User>(newUser);
                await _unitOfWork.UserRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    if (newUser.RoleId == 6)
                    {
                        var staff = new User
                        {
                            Username = insertedUser.Username,
                            Fullname = newUser.Fullname,
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
                        await _unitOfWork.UserRepository.InsertAsync(staff);
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

        public async Task<bool> CreateUserModerator(UserView newUser)
        {
            try
            {
                bool status = false;
                newUser.Status = "active";
                newUser.RoleId = 5;
                var user = _mapper.Map<User>(newUser);
                await _unitOfWork.UserRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    if (newUser.RoleId == 5)
                    {
                        var staff = new User
                        {
                            Username = insertedUser.Username,
                            Fullname = newUser.Fullname,
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
                        await _unitOfWork.UserRepository.InsertAsync(staff);
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
    }
}
