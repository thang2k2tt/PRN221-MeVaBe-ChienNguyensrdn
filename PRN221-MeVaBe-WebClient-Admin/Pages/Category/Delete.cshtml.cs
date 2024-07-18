using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_Repo.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private IUnitOfWork unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ProductCategory ProductCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            

            //if (HttpContext.Session.GetString("role") == "2")
            //{
            //    return RedirectToPage("/Products/Index");
            //}
            
            if (id == null)
            {
                return NotFound();
            }

            ProductCategory = unitOfWork.ProductCategoryRepository.GetByID(id);

            if (ProductCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductCategory = unitOfWork.ProductCategoryRepository.GetByID(id);
            

            if (ProductCategory != null)
            {
                unitOfWork.ProductCategoryRepository.Delete(ProductCategory);
                unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
