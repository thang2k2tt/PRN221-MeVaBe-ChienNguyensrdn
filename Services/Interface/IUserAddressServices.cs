using PRN221_MeVaBe_Repo.DTOs;
using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserAddressServices
    {
        Task<List<UserAddress>> GetAllUserAddres();
        Task AddAddress(CreateAddressDTO addressDTO);
        Task UpdateAddress(UpdateAddressDTO addressDTO);
        Task DeleteAddress(int id);
    }
}
