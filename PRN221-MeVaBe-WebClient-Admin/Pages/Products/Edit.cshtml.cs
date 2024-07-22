using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_Repo.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile CoverImageFile { get; set; }

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            Product = _unitOfWork.ProductRepository.GetByID(id);
            if (Product == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_unitOfWork.ProductCategoryRepository.Get(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            _unitOfWork.ProductRepository.Update(Product);
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
