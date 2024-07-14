using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Models;
using Services.Interface;

namespace PRN221_MeVaBe_WebClient.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserServices _userServices;

        public DetailsModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public User User { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _userServices.GetUserById(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

    }
}
