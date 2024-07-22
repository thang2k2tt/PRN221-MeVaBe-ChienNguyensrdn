﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class EditModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public string message { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            bool isValid = true;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (ProductCategory.Name.Length<10)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            if (!isValid)
            {
                message = "Update failed";
                return Page();
            }
            
            unitOfWork.ProductCategoryRepository.Update(ProductCategory);
            
            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeglassExists(ProductCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EyeglassExists(int id)
        {

            return unitOfWork.ProductRepository.GetByID(id) != null;
        }
        
    }
}