using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

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
        public IList<OrderItem> ProductBestSeller { get; set; }
        public IList<Product> ProductSeller { get; set; }
        public double? Rate { get; set; }
        public string paymentWithCODJson { get; set; }
        public string paymentWithVNPayJson { get; set; }
        public string orderCompleteJson { get; set; }
        public string orderPendingJson { get; set; }
        public void OnGet()
        {
            var product = unitOfWork.ProductRepository.Get();
            ProductCount = product.Count();
            ProductSeller = new List<Product>();
            var user = unitOfWork.UserRepository.Get(filter: u => u.Status == true);
            ProductBestSeller = (IList<OrderItem>)unitOfWork.OrderItemRepository.Get(orderBy: q => q
                .GroupBy(oi => oi.ProductId)
                .Select(g => new OrderItem
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(x => x.Quantity));
            UserActive = user.Count();
            foreach(var items in ProductBestSeller)
            {
                var tmp = unitOfWork.ProductRepository.GetByID(items.ProductId);
                ProductSeller.Add(tmp);
            }
            var feedback = unitOfWork.FeedbackRepository.Get();
            double? sum = 0;
            if (feedback.Count() > 0)
            {
                foreach (var items in feedback)
                {
                    sum += items.Rate;
                }
                Rate = (sum / feedback.Count()) * 10;
            }
            var payment = unitOfWork.PaymentRepository.Get(includeProperties: "OrderDetail");
            List<Payment> COD = new List<Payment>();
            List<Payment> VNPay = new List<Payment>();
            foreach (var item in payment)
            {
                if(item.TypePayment == "COD")
                {
                    COD.Add(item);
                }else if(item.TypePayment == "VNPay")
                {
                    VNPay.Add(item);
                }
            }
            List<Int32> PaymentWithVNPay = totalPaymentByMonth(VNPay);
            List<Int32> PaymentWithCOD = totalPaymentByMonth(COD);
            
            paymentWithCODJson = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentWithCOD);
            paymentWithVNPayJson = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentWithVNPay);

            List<OrderDetail> orderComplete = (List<OrderDetail>)unitOfWork.OrderDetailRepository.Get(filter: o => o.OrderSatus == "Complete");
            List<OrderDetail> orderPending = (List<OrderDetail>)unitOfWork.OrderDetailRepository.Get(filter: o => o.OrderSatus == "Pending");

            orderCompleteJson = Newtonsoft.Json.JsonConvert.SerializeObject(totalOrderByMonth(orderComplete));
            orderPendingJson = Newtonsoft.Json.JsonConvert.SerializeObject(totalOrderByMonth(orderPending));
        }
        private List<Int32> totalPaymentByMonth(List<Payment> payments)
        {
            List<Int32> total = [0,0,0,0,0,0,0,0,0,0,0,0];
            for (int i = 1; i <= 12; i++)
            {
                int sum = 0;
                foreach (var items in payments)
                {
                    DateTime tmp = (DateTime)items.OrderDetail.CreateDate;
                    DateTime _tmp = new DateTime(2024, i, 1);
                    if(tmp.Month == _tmp.Month)
                    {
                        sum++;
                    }
                }
                total[i-1] = sum;
            }
            
            return total;
        }
        private List<Int32> totalOrderByMonth(List<OrderDetail> orderDetails)
        {
            List<Int32> total = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            for (int i = 1; i <= 12; i++)
            {
                int sum = 0;
                foreach (var items in orderDetails)
                {
                    DateTime tmp = (DateTime)items.CreateDate;
                    DateTime _tmp = new DateTime(2024, i, 1);
                    if (tmp.Month == _tmp.Month)
                    {
                        sum++;
                    }
                }
                total[i - 1] = sum;
            }
            return total;
        }
    }
}
