using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;

namespace PRN221_MeVaBe_WebClient_Adminn.Pages
{
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public int ProductCount { get; set; }
        public int UserActive { get; set; }
        public double? Rate { get; set; }
        public void OnGet()
        {
            var product = unitOfWork.ProductRepository.Get();
            ProductCount = product.Count();
            var user = unitOfWork.UserRepository.Get(filter: u => u.Status == true);
            UserActive = user.Count();
            var feedback = unitOfWork.FeedbackRepository.Get();
            double? sum = 0;
            if (feedback.Count() > 0)
            {
                foreach (var items in feedback)
                {
                    sum += items.Rate;
                }
                Rate = (sum / feedback.Count())*10;
            }

        }
    }
}
