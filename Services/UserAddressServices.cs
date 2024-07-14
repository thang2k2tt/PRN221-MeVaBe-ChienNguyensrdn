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
    public class UserAddressServices : IUserAddressServices
    {
        private readonly IUserAAddressRepository _repository;
        private readonly IUserRepository _userRepository;

        public UserAddressServices(IUserAAddressRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<List<UserAddress>> GetAllUserAddres()
        {
            return await _repository.GetAllUserAddress();
        }

        public async Task AddAddress(CreateAddressDTO addressDTO)
        {
            var user = await _userRepository.GetUserByid(addressDTO.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }
            var userAddress = new UserAddress()
            {
                UserId = user.Id,
                Province = addressDTO.Address,
                District = addressDTO.District,
                Ward = addressDTO.Ward,
                Address = addressDTO.Address,
            };
            await _repository.AddAddress(userAddress);
        }

        public async Task UpdateAddress( UpdateAddressDTO addressDTO)
        {
            var userAddress = await _repository.GetUserAddressByUserId(addressDTO.Id);
            if (userAddress == null)
            {
                throw new ArgumentException("Address not found for the given user ID");
            }          
            userAddress.Province = addressDTO.Address;
            userAddress.District = addressDTO.District;
            userAddress.Ward = addressDTO.Ward;
            userAddress.Address = addressDTO.Address;

            await _repository.UpdateAddress(userAddress);
        }

        public async Task DeleteAddress(int id)
        {       
            var address = await _repository.GetUserAddressById(id);
            if(address != null)
            {
               await _repository.DeleteAddress(id);  
            }
        }
    }
}
