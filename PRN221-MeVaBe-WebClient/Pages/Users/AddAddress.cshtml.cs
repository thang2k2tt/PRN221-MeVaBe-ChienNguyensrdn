using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.DTOs;
using Services;
using Services.Interface;

namespace PRN221_MeVaBe_WebClient.Pages.Users
{
    public class AddAddressModel : PageModel
    {
        private readonly IUserAddressServices _userAddressServices;

        [BindProperty]
        public CreateAddressDTO AddressDTO { get; set; }

        public AddAddressModel(IUserAddressServices userAddressServices)
        {
            _userAddressServices = userAddressServices;
        }

        public void OnGet(int userId)
        {
            AddressDTO = new CreateAddressDTO { UserId = userId };
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
                return RedirectToPage("/HomePage");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
