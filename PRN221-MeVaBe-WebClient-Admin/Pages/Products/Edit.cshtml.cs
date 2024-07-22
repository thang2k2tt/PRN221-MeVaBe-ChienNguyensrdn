using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile CoverImageFile { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            if (!ModelState.IsValid)
            {
                ViewData["Id"] = new SelectList(_unitOfWork.ProductCategoryRepository.Get(), "Id", "Name");
                return Page();
            }

            if (CoverImageFile != null)
            {
                var fileName = Path.GetFileName(CoverImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await CoverImageFile.CopyToAsync(fileStream);
                }

                Product.CoverImage = "/uploads/" + fileName;
            }

            _unitOfWork.ProductRepository.Update(Product);
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
