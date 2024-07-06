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
    public class UserAddressRepository : IUserAAddressRepository
    {
        private readonly DBContext _context;

        public UserAddressRepository(DBContext context)
        {
            _context = context;
        }

        public async Task AddAddress(UserAddress userAddress)
        {
            _context.TblUserAddresses.Add(userAddress);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(int id)
        {
            var address = await GetUserAddressById(id);
            if (address != null)
            {
                _context.TblUserAddresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserAddress>> GetAllUserAddress()
        {
            return await _context.TblUserAddresses.ToListAsync();
        }

        public async Task<UserAddress> GetUserAddressById(int id)
        {
            return await _context.TblUserAddresses.FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task UpdateAddress(UserAddress userAddress)
        {
            _context.TblUserAddresses.Update(userAddress);
            await _context.SaveChangesAsync();
        }
    }
}
