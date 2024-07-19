using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.DTOs;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using Services;
using Services.Interface;

namespace PRN221_MeVaBe_WebClient.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserAddressServices _userAddressServices;
        private readonly IUserServices _userServices;
        private IUnitOfWork unitOfWork;

        public DetailsModel(IUserServices userServices, IUnitOfWork unitOfWork, IUserAddressServices userAddressServices)
        {
            _userServices = userServices;
            this.unitOfWork = unitOfWork;
            _userAddressServices = userAddressServices;
        }

        public User User { get; private set; }
        public List<UserAddress> Address { get; private set; }
        public List<OrderItem> OrderItem { get; private set; }
        [BindProperty]
        public CreateAddressDTO AddressDTO { get; set; }
        public async Task<IActionResult> OnGetAsync(int userId)
        {
            User = await _userServices.GetUserById(userId);
            Address = (List<UserAddress>)unitOfWork.UserAddressRepository.Get(filter: a => a.UserId == userId);
            var order = unitOfWork.OrderDetailRepository.Get(filter: o => o.UserId == userId);
            List<OrderItem> orderItems = new List<OrderItem>();
            if(order.Count() > 0)
            {
                foreach(var items in order)
                {
                    List<OrderItem> orderItemsList = (List<OrderItem>)unitOfWork.OrderItemRepository.Get(filter: o => o.OrderDetailId == items.Id, includeProperties:"Product");
                    orderItems.AddRange(orderItemsList) ;
                }
            }
            OrderItem = orderItems;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _userAddressServices.AddAddress(AddressDTO);
                return RedirectToPage("https://localhost:7090/Users/Details?handler=OnGetAsync&userId=" + AddressDTO.UserId);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
