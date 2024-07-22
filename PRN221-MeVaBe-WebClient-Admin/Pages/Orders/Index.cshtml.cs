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

namespace PRN221_MeVaBe_WebClient_Adminn.Pages.Orders
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public async Task OnGetAsync()
        {
            OrderDetail = (IList<OrderDetail>)unitOfWork.OrderDetailRepository.Get(includeProperties: "User");
        }

        public JsonResult OnPostGetItems(int orderDetailId)
        {
            var orderItemList = unitOfWork.OrderItemRepository.Get(filter: o => o.OrderDetailId == orderDetailId, includeProperties: "Product");
            return new JsonResult(new { success = true, data = orderItemList });
        }
        public JsonResult OnPostUserDetail(int userId)
        {
            var user = unitOfWork.UserRepository.GetByID(userId);
            return new JsonResult(new { success = true, data = user });
        }

        public JsonResult OnPostUpdateStatus(int orderDetailId, string status)
        {
            var order = unitOfWork.OrderDetailRepository.GetByID(orderDetailId);
            order.OrderSatus = status;
            unitOfWork.OrderDetailRepository.Update(order);
            unitOfWork.Save();
            return new JsonResult(new { success = true });
        }
    }
}
