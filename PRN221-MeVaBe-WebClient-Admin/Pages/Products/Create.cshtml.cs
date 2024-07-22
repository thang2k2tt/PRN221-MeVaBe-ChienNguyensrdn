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
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile CoverImageFile { get; set; }

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            Product = new Product();
            ViewData["Id"] = new SelectList(_unitOfWork.ProductCategoryRepository.Get(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Product.IdNavigation = null;
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Id"] = new SelectList(_unitOfWork.ProductCategoryRepository.Get(), "Id", "Name");
            //    return Page();
            //}


            if (CoverImageFile != null && CoverImageFile.Length > 0)
            {
                // Đường dẫn đến folder img trong wwwroot
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                // Tạo thư mục nếu nó chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + CoverImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Lưu file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await CoverImageFile.CopyToAsync(fileStream);
                }
                Product.CoverImage = uniqueFileName;
                var productList = _unitOfWork.ProductRepository.Get().ToList();
                Product.Id = productList.Any() ? productList.Max(p => p.Id) + 1 : 1;
                Product.Status = true;

                _unitOfWork.ProductRepository.Insert(Product);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}