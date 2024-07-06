using PRN221_MeVaBe_Repo.DTOs;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserServices: IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserByid(id);
        }

        public async Task<User> Login(string username, string password)
        {
            return await _userRepository.Login(username, password);
        }

        public async Task<bool> UpdatePassword(int id, string oldpassword, string newpassword)
        {
            try
            {

                var user = await _userRepository.GetUserByid(id);
                if (user == null)
                {
                    throw new Exception("Not have this Account in system");
                }
                if (user != null && user.Password == oldpassword)
                {
                    return await _userRepository.UpdateUsersPassword(id, newpassword);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> CreateAccount(CreateAccountDTO createAccountDTO)
        {
            try
            {
                if (createAccountDTO == null)
                {
                    return "Account not found";
                }
             
                if (string.IsNullOrWhiteSpace(createAccountDTO.Email) || string.IsNullOrWhiteSpace(createAccountDTO.Password))
                {
                    return "Email and Password need required.";
                }             
                var account = new User()
                {
                    Email = createAccountDTO.Email,
                    Password = createAccountDTO.Password,
                    Fullname = createAccountDTO.Fullname,
                    Dob = createAccountDTO.Dob,
                    Phone = createAccountDTO.Phone,
                };
                await _userRepository.CreateUser(account);
                return "Create Success";
            }
            catch (Exception ex) 
            {             
                throw new InvalidOperationException("Can Not Create Account", ex);
            }
        }

        public async Task<string> DeleteAccount(int userId)
        {
            var user = _userRepository.GetUserByid(userId);
            if (user == null)
            {
                return "User not found";
            }
            await _userRepository.DeleteAccount(userId);
            return "Delete Success";
        }

        public async Task<string> UpdateAccount(int userId, UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO == null)
            {
                return "UpdateUserDTO cannot be null.";
            }
            var user = await _userRepository.GetUserByid(userId);
            if (user == null)
            {
                return "User not found.";
            }

            user.Email = updateUserDTO.Email ?? user.Email;
            user.Phone = updateUserDTO.Phone ?? user.Phone;
            user.Avatar = updateUserDTO.Avatar ?? user.Avatar;
            user.Dob = updateUserDTO.Dob ?? user.Dob;
            user.Fullname = updateUserDTO.Fullname ?? user.Fullname;

            await _userRepository.UpdateAccount(user);
            return "Update User Success";
        }
    }

}
