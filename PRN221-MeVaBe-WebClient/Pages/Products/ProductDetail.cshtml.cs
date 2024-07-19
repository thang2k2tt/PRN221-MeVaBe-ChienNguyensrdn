using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Products
{
    [IgnoreAntiforgeryToken]
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
        public JsonResult OnPostCreateFeedback(int userId, int productId, string content, int rating)
        {
            var order = unitOfWork.OrderDetailRepository.Get(filter: c => c.UserId == userId);
            if(order == null)
            {
                return new JsonResult(new { success = false });

            }
            List<OrderItem> items = new List<OrderItem>();
            foreach(var item in order)
            {
                var itemItem = unitOfWork.OrderItemRepository.Get(filter: o => o.OrderDetailId == item.Id);
                items.AddRange(itemItem);
            }
            bool checkBuy = false;
            foreach(var item in items)
            {
                if(item.ProductId == productId)
                {
                    checkBuy = true;
                    break;
                }
            }
            if (checkBuy)
            {
                int id = 1;
                if(unitOfWork.FeedbackRepository.Get().Count()  > 0)
                {
                    id = unitOfWork.FeedbackRepository.Get().LastOrDefault().Id + 1;
                }
                Feedback feedback = new Feedback
                {
                    Id = id,
                    Content = content,
                    Rate = rating,
                    ProductId = productId,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                };
                unitOfWork.FeedbackRepository.Insert(feedback);
                unitOfWork.Save();
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }
    }
}
