using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient_Adminn.Pages.Feedbacks
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<Feedback> Feedback { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Feedback = (IList<Feedback>)unitOfWork.FeedbackRepository.Get(includeProperties: "User");
        }

        
        

        
    }
}
