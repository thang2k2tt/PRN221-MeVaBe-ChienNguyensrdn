using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Products
{
    public class ProductDetailModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public ProductDetailModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Product Product { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Feedback> Feedbacks { get; set; }
        public double? totalRate {  get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
        public IActionResult OnGet(int productId)
        {
            Product = unitOfWork.ProductRepository.GetByID(productId);
            Feedbacks = (IList<Feedback>)unitOfWork.FeedbackRepository.Get(filter: f => f.ProductId == productId, includeProperties: "User");
            foreach(var items in Feedbacks)
            {
                totalRate += items.Rate;
            }
            if(totalRate > 0)
            {
                totalRate = totalRate / Feedbacks.Count;
            }
            Products = (IList<Product>)unitOfWork.ProductRepository.Get(filter: p => p.ProductCategoryId == Product.ProductCategoryId, includeProperties: "ProductCategory");
            ProductCategories = (IList<ProductCategory>)unitOfWork.ProductCategoryRepository.Get();
            return Page();
        }
    }
}
