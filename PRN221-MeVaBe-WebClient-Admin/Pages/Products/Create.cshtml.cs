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
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile CoverImageFile { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            Product = new Product();
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

            // Handle file upload
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

            var productList = _unitOfWork.ProductRepository.Get().ToList();
            Product.Id = productList.Any() ? productList.Max(p => p.Id) + 1 : 1;
            Product.Status = true;

            _unitOfWork.ProductRepository.Insert(Product);
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}