using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_Repo.Pages.Category
{
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public int pageIndex { get; set; } = 1;
        public int pageIndexMax { get; set; }
        public string role { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<ProductCategory> ProductCategory { get; set; }

        public IActionResult OnGet(int pageIndex, string textSearch,string textSearch1,string textSearch2)
        {
            
            role = "1";
            if (textSearch == null && textSearch1==null && textSearch2==null)
            {
                pageIndexMax = (int)Math.Ceiling((double)unitOfWork.ProductCategoryRepository.Get().Count() / 4);
                ProductCategory = (IList<ProductCategory>)unitOfWork.ProductCategoryRepository.Get(orderBy: l => l.OrderByDescending(e => e.Id), includeProperties: "", pageIndex: pageIndex, pageSize: 5);
            }
            //if(textSearch1 == null && textSearch2==null &&textSearch!=null)
            //{
            //    if (decimal.TryParse(textSearch, out decimal decimalValue))
            //    {
            //        Product = (IList<Product>)unitOfWork.ProductRepository.Get(p => p. == decimalValue, orderBy: l => l.OrderByDescending(e => e.EqId), includeProperties: "Room", pageIndex: pageIndex, pageSize: 5);
            //    }
            //    else
            //    {
            //        Product = (IList<Product>)unitOfWork.ProductRepository.Get(p => p.EqName.Contains(textSearch), orderBy: l => l.OrderByDescending(e => e.EqId), includeProperties: "Room", pageIndex: pageIndex, pageSize: 5);
            //    }
            //    pageIndexMax = (int)Math.Ceiling((double)Product.Count() / 4);
            //}
           


            return Page();
        }
    }
}
