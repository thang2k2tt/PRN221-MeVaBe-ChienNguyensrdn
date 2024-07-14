using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Interfaces
{
    public interface IUserAAddressRepository
    {
        Task<List<UserAddress>> GetAllUserAddress();
        Task<UserAddress> GetUserAddressById(int id);
        Task<UserAddress> GetUserAddressByUserId(int userId);
        Task AddAddress(UserAddress userAddress);
        Task UpdateAddress(UserAddress userAddress);
        Task DeleteAddress(int id);

    }
}
