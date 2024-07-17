using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class CheckoutModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public CheckoutModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<CartItem> CartItems { get; set; }
        public IList<UserAddress> _UserAddress { get; set; }
        public PRN221_MeVaBe_Repo.Models.Cart _Cart { get; set; }
        public int AddressId { get; set; }
        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
            {
                return RedirectToPage("/Users/Login");
            }
            _UserAddress = (IList<UserAddress>)unitOfWork.UserAddressRepository.Get(filter: a => a.UserId == userId);
            _Cart = (PRN221_MeVaBe_Repo.Models.Cart)unitOfWork.CartRepository.Get(filter: c => c.UserId == userId).FirstOrDefault();
            CartItems = (IList<CartItem>)unitOfWork.CartItemRepository.Get(filter: c => c.CartId == _Cart.Id, includeProperties: "Product");

            return Page();
        }
        public ActionResult OnPost(int addressSelect, string province, string district, string ward, string address, string payment, int cartId)
        {
            var cart = unitOfWork.CartRepository.GetByID(cartId);
            var cartItem = unitOfWork.CartItemRepository.Get(filter: c => c.CartId == cart.Id, includeProperties: "Product").ToList();
            var list = unitOfWork.OrderDetailRepository.Get();
            int id = 1;
            if (list.Count() > 0)
            {
                id = list.LastOrDefault().Id + 1;
            }

            OrderDetail orderDetail = new OrderDetail
            {
                Id = id,
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                UserId = cart.UserId,
                Total = cart.Total,
                OrderSatus = "Pending"
            };
            if (addressSelect == 0)
            {
                orderDetail.Phone = address + "," + ward + "," + district + "," + province;
            }
            else
            {
                var _address = unitOfWork.UserAddressRepository.GetByID(addressSelect);
                orderDetail.Phone = _address.Address + "," + _address.Ward + "," + _address.District + "," + _address.Province;
            }
            unitOfWork.OrderDetailRepository.Insert(orderDetail);
            unitOfWork.Save();

            int paymentId = 1;
            var paymentList = unitOfWork.PaymentRepository.Get();
            if (paymentList.Count() > 0)
            {
                paymentId = paymentList.LastOrDefault().Id + 1;
            }
            bool paymentStatus = true;
            if (payment == "COD")
            {
                paymentStatus = false;
            }
            Payment _payment = new Payment
            {
                Id = paymentId,
                OrderDetailId = id,
                Total = cart.Total,
                Status = paymentStatus,
                TypePayment = payment
            };
            unitOfWork.PaymentRepository.Insert(_payment);
            unitOfWork.Save();

            
            foreach (var items in cartItem)
            {
                int idOrderItem = 1;
                var orderItemList = unitOfWork.OrderItemRepository.Get();
                if (orderItemList.Count() > 0)
                {
                    idOrderItem = orderItemList.LastOrDefault().Id + 1;
                }
                OrderItem o = new OrderItem
                {
                    Id = idOrderItem,
                    OrderDetailId = id,
                    Price = items.Product.Price,
                    Quantity = items.Quanity,
                    ProductId = items.ProductId
                };
                unitOfWork.OrderItemRepository.Insert(o); 
                unitOfWork.Save();
            }
            foreach (var items in cartItem)
            {
                unitOfWork.CartItemRepository.Delete(items);
                unitOfWork.Save();
            }
            cart.Total = 0;
            unitOfWork.CartRepository.Update(cart);
            unitOfWork.Save();
            HttpContext.Session.SetString("payment","success");
            return Redirect("/HomePage");
        }

        public JsonResult OnPostGetAddress(int addressId)
        {
            var address = unitOfWork.UserAddressRepository.GetByID(addressId);
            return new JsonResult(new { success = true, data = address });
        }
    }
}
