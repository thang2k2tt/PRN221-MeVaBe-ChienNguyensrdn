using PRN221_MeVaBe_Repo.DTOs;
using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserServices
    {
        Task<User> Login(string username, string password);
        Task<User> GetUserById(int id);
        Task<bool> UpdatePassword(int id, string oldpassword, string newpassword);
        Task<string> CreateAccount(CreateAccountDTO createAccountDTO);
        Task<string> DeleteAccount(int userId);
        Task<string> UpdateAccount(int userId, UpdateUserDTO updateUserDTO);
    }
}
