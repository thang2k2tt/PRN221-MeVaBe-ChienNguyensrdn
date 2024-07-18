using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using PRN221_MeVaBe_Repo.Repositories;

namespace WebApplication5.Pages.Category
{
    public class CreateModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public string message { get; set; }
        [BindProperty]
        public ProductCategory ProductCategory { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {


            //if (HttpContext.Session.GetString("role") == "2")
            //{
            //    return RedirectToPage("/Products/Index");
            //}
            ProductCategory = new ProductCategory();
            
            
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            
            
            bool isValid = true;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<ProductCategory> list = (List<ProductCategory>)unitOfWork.ProductCategoryRepository.Get();
            ProductCategory.Id = list.Last().Id + 1;
            if (ProductCategory.Name.Length > 10)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }
            if (!isValid)
            {
                message = "Create failed";
                return Page();
            }

            
            unitOfWork.ProductCategoryRepository.Insert(ProductCategory);
            unitOfWork.Save();

            return RedirectToPage("./Index");
        }

        //static bool IsValidEyeglassesName(string eyeglassesName)
        //{
        //    // Check if the length is greater than 10 characters
        //    if (eyeglassesName.Length <= 10)
        //    {
        //        return false;
        //    }

        //    // Check if each word begins with a capital letter
        //    string[] words = eyeglassesName.Split(' ');

        //    foreach (string word in words)
        //    {
        //        if (string.IsNullOrEmpty(word) || !char.IsUpper(word[0]))
        //        {
        //            return false;
        //        }
        //    }

        //    // If both conditions are met, return true
        //    return true;
        //}
    }
}
