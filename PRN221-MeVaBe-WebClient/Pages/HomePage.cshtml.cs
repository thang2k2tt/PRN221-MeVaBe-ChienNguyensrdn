using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages
{
    public class HomePageModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public HomePageModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<Product> Products { get; set; } = new List<Product>();
        public IList<Product> BestSellerProducts { get; set; }
        public IActionResult OnGet()
        {
            for(int i = 1; i <= 4; i++)
            {
                IList<Product> _Products = (IList<Product>)unitOfWork.ProductRepository.Get(filter: c => c.ProductCategoryId == i, includeProperties: "ProductCategory", pageIndex: 1, pageSize: 2);
                for(int j = 0; j < _Products.Count; j++)
                {
                    Products.Add(_Products[j]);
                }
            }
            BestSellerProducts = (IList<Product>)unitOfWork.ProductRepository.Get(includeProperties: "ProductCategory", pageIndex: 1, pageSize: 6);
            return Page();
        }
    }
}
