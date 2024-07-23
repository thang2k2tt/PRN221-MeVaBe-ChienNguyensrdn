using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<User> GetUserByid(int? id);
        Task<bool> UpdateUsersPassword(int id, string password);
        Task CreateUser(User user);
        Task DeleteAccount(int userId);
        Task UpdateAccount(User user);
    }
}
