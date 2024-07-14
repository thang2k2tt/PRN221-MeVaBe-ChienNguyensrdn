using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DBContext _dbContext;

        public UserRepository(DBContext context)
        {
            _dbContext = context;
        }

        public async Task CreateUser(User user)
        {
            _dbContext.TblUsers.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(int userId)
        {
            var user = await GetUserByid(userId);
            if (user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByid(int? id)
        {
            return await _dbContext.TblUsers.FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<User> Login(string email, string password)
        {
            return await _dbContext.TblUsers.FirstOrDefaultAsync(sc => sc.Email == email && sc.Password == password); ;
        }

        public async Task UpdateAccount(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateUsersPassword(int id, string password)
        {
            var user = await _dbContext.TblUsers.FindAsync(id);
            if (user == null)
            {
                user.Password = password;
                _dbContext.TblUsers.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
