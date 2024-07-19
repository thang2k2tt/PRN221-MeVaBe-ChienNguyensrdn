using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Libraries;
using PRN221_MeVaBe_Repo.Models;
using PRN221_MeVaBe_Repo.Repositories;

namespace PRN221_MeVaBe_WebClient.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class CheckoutModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CheckoutModel(IUnitOfWork unitOfWork, IHttpContextAccessor _httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = _httpContextAccessor;
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
                Status = false,
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
            if (payment == "COD")
            {
                HttpContext.Session.SetString("payment", "success");
                return Redirect("/HomePage");
            }
            HttpContext context = _httpContextAccessor.HttpContext;

            return Redirect(CreatePaymentUrl(context, id, paymentId, orderDetail.UserId, _payment.Total));
        }

        public JsonResult OnPostGetAddress(int addressId)
        {
            var address = unitOfWork.UserAddressRepository.GetByID(addressId);
            return new JsonResult(new { success = true, data = address });
        }
        public string CreatePaymentUrl(HttpContext context, int orderId, int paymentId, int userId, double? amount)
        {
            var request = HttpContext.Request;
            var user = unitOfWork.UserRepository.GetByID(userId);
            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnPayLibrary();
            string payBackUrl = $"{request.Scheme}://{request.Host}/Cart/Checkout?handler=PaymentCallBack";

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", "W1RE8VOR");
            vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString());

            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + $"paymentId:{paymentId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", payBackUrl);
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

            TimeZoneInfo timeZone = TimeZoneInfo.Utc;
            DateTime utcTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZone);
            // Thêm 7 giờ và 15 phút vào thời gian hiện tại
            DateTime expireTime = utcTime.AddHours(7).AddMinutes(15);
            // Định dạng thời gian theo yêu cầu
            string vnp_ExpireDate = expireTime.ToString("yyyyMMddHHmmss");
            vnpay.AddRequestData("vnp_ExpireDate", vnp_ExpireDate);

            var paymentUrl = vnpay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "8FS574OKMGRTLOCOBM8YBQ8K22YHS5ZJ");
            return paymentUrl;
        }
        public async Task<IActionResult> OnGetPaymentCallBack()
        {
            var queryParameters = HttpContext.Request.Query;
            string orderInfo = queryParameters["vnp_OrderInfo"];
            string paymentId = this.getPaymentId(orderInfo);
            if (string.IsNullOrEmpty(orderInfo))
            {
                return BadRequest("Thông tin đơn hàng không tồn tại.");
            }

            var orderInfoDict = new Dictionary<string, string>();
            string[] pairs = orderInfo.Split(',');
            foreach (var pair in pairs)
            {
                string[] keyValue = pair.Split(':');
                if (keyValue.Length == 2)
                {
                    orderInfoDict[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            var payment = unitOfWork.PaymentRepository.GetByID(int.Parse(paymentId));
            payment.Status = true;
            unitOfWork.PaymentRepository.Update(payment);
            unitOfWork.Save();
            
            HttpContext.Session.SetString("payment", "success");
            return Redirect("/HomePage");

        }
        private string getPaymentId(string orderInfo)
        {
            string details = orderInfo.Substring(orderInfo.IndexOf(':') + 1);

            var keyValuePairs = details.Split(',');

            var dict = new Dictionary<string, string>();

            foreach (var pair in keyValuePairs)
            {
                var keyValue = pair.Split(':');
                if (keyValue.Length == 2)
                {
                    dict[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            // Lấy UserID và Amount từ dictionary
            if (dict.TryGetValue("paymentId", out string paymentId))
            {
                return paymentId;
            }
            else
            {
                return null;
            }
        }
    }
}
    
