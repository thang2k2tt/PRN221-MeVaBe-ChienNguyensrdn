using PRN221_MeVaBe_Repo.Models;
using PRN221_MeVaBe_Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public GenericRepository<Blog> BlogRepository { get; }
        public GenericRepository<BlogCategory> BlogCategoryRepository { get; }
        public GenericRepository<Cart> CartRepository { get; }
        public GenericRepository<CartItem> CartItemRepository { get; }
        public GenericRepository<Feedback> FeedbackRepository { get; }
        public GenericRepository<OrderDetail> OrderDetailRepository { get; }
        public GenericRepository<OrderItem> OrderItemRepository { get; }
        public GenericRepository<Payment> PaymentRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<ProductCategory> ProductCategoryRepository { get; }
        public GenericRepository<ProductSubImage> ProductSubImageRepository { get; }
        public GenericRepository<User> UserRepository { get; }
        public GenericRepository<UserAddress> UserAddressRepository { get; }
        public void Save();
    }
}
