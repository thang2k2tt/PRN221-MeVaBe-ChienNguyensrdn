using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.DTOs;
using Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PRN221_MeVaBe_WebClient.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly IUserServices _userServices;

        public RegisterModel(IUserServices userServices)
        {
            _userServices = userServices;
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

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Fullname { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateOnly Dob { get; set; }

            [Required]
            [Phone]
            public string Phone { get; set; }
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

            var createAccountDTO = new CreateAccountDTO
            {
                Email = Input.Email,
                Password = Input.Password,
                Fullname = Input.Fullname,
                Dob = Input.Dob,
                Phone = Input.Phone
            };

            var result = await _userServices.CreateAccount(createAccountDTO);

            if (result != "Create Success")
            {
                ModelState.AddModelError(string.Empty, "Không thể tạo tài khoản.");
                return Page();
            }

            return RedirectToPage("/Users/Login");
        }
    }
}
