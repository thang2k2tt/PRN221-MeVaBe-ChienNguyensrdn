using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using System.Linq.Expressions;

namespace PRN221_MeVaBe_WebClient.Pages
{
    public class ShopModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public int pageIndex { get; set; }
        public int pageIndexDefault { get; set; } = 1;
        public int pageIndexCurrent { get; set; } = 1;
        public string _keywords {  get; set; }
        public int? _categoryId { get; set; } = 0;
        public string _sortBy { get; set; }
        public ShopModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<Product> Products { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
        public IActionResult OnGet(int pageCurrent, string? keywords, int? categoryId, string? sortBy)
        {
            if(keywords != null)
            {
                _keywords = keywords;

            }
            if(categoryId != null)
            {
                _categoryId = categoryId;
            }
            if(sortBy != null)
            {
                _sortBy = sortBy;
            }
            Expression<Func<Product, bool>> filter = p => (keywords == null || p.ProductName.Contains(keywords)) && (categoryId == null || p.ProductCategoryId == categoryId);
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;
            if(sortBy == null)
            {

            }else if (sortBy.Equals("priceAsc"))
            {
                orderBy = q => q.OrderBy(p => p.Price);
            }
            else if (sortBy.Equals("priceDesc"))
            {
                orderBy = q => q.OrderByDescending(p => p.Price);
            }
            if (pageCurrent == 0)
            {
                Products = (IList<Product>)unitOfWork.ProductRepository.Get(includeProperties: "ProductCategory",filter: filter, orderBy: orderBy, pageIndex: pageIndexDefault, pageSize: 9);
                var _Products = (IList<Product>)unitOfWork.ProductRepository.Get(includeProperties: "ProductCategory", filter: filter, orderBy: orderBy);
                pageIndex = unitOfWork.ProductRepository.Count(_Products, 9);
                ProductCategories = (IList<ProductCategory>)unitOfWork.ProductCategoryRepository.Get();

            }
            else
            {
                Products = (IList<Product>)unitOfWork.ProductRepository.Get(includeProperties: "ProductCategory", filter: filter, orderBy: orderBy, pageIndex: pageCurrent, pageSize: 9);
                var _Products = (IList<Product>)unitOfWork.ProductRepository.Get(includeProperties: "ProductCategory", filter: filter, orderBy: orderBy);
                pageIndex = unitOfWork.ProductRepository.Count(_Products, 9);
                ProductCategories = (IList<ProductCategory>)unitOfWork.ProductCategoryRepository.Get();
                pageIndexCurrent = pageCurrent;
            }

            return Page();
        }
    }
}
