using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public CheckoutModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<CartItem> CartItems { get; set; }
        public IList<UserAddress> _UserAddress { get; set; }
        public PRN221_MeVaBe_Repo.Models.Cart _Cart { get; set; }
        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
            {
                return RedirectToPage("/Users/Login");
            }
            _UserAddress = (IList<UserAddress>)unitOfWork.UserAddressRepository.Get(filter: a => a.UserId == userId);
            _Cart = (PRN221_MeVaBe_Repo.Models.Cart)unitOfWork.CartRepository.Get(filter: c => c.UserId == userId).FirstOrDefault();
            CartItems = (IList<CartItem>)unitOfWork.CartItemRepository.Get(filter: c => c.CartId == _Cart.Id, includeProperties: "Product");
            
            return Page();
        }
    }
}
