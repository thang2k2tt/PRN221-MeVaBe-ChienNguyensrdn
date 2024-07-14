using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PRN221_MeVaBe_WebClient.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly IUserServices _userServices;
        private IUnitOfWork unitOfWork;

        public LoginModel(IUserServices userServices, IUnitOfWork unitOfWork)
        {
            _userServices = userServices;
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userServices.Login(Input.Email, Input.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ.");
                return Page();
            }
            HttpContext.Session.SetInt32("userId", user.Id);
            var cart = unitOfWork.CartRepository.Get(c => c.UserId == user.Id);
            if (cart.Count() == 0)
            {
                HttpContext.Session.SetInt32("cartItem", 0);
            }
            else
            {
                int cartId = cart.FirstOrDefault().Id;
                var cartItem = unitOfWork.CartItemRepository.Get(c => c.CartId == cartId);
                if (cartItem.Count() == 0)
                {
                    HttpContext.Session.SetInt32("cartItem", 0);
                }
                else
                {
                    HttpContext.Session.SetInt32("cartItem", cartItem.Count());
                }
            }
            // Đăng nhập thành công, chuyển hướng đến trang khác
            return RedirectToPage("/HomePage");
        }
    }
}
