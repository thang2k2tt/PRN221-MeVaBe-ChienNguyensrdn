using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext _context = new DBContext();
        public GenericRepository<Blog> _blogRepository;
        public GenericRepository<BlogCategory> _blogCategoryRepository;
        public GenericRepository<Cart> _cartRepository;
        public GenericRepository<CartItem> _cartItemRepository;
        public GenericRepository<Feedback> _feedbackRepository;
        public GenericRepository<OrderDetail> _orderDetailRepository;
        public GenericRepository<OrderItem> _orderItemRepository;
        public GenericRepository<Payment> _paymentRepository;
        public GenericRepository<Product> _productRepository;
        public GenericRepository<ProductCategory> _productCategoryRepository;
        public GenericRepository<ProductSubImage> _productSubImageRepository;
        public GenericRepository<User> _userRepository;
        public GenericRepository<UserAddress> _userAddressRepository;
        public GenericRepository<Blog> BlogRepository
        {
            get
            {
                if (this._blogRepository == null)
                {
                    this._blogRepository = new GenericRepository<Blog>(_context);
                }
                return this._blogRepository;
            }
        }

        public GenericRepository<BlogCategory> BlogCategoryRepository
        {
            get
            {
                if (this._blogCategoryRepository == null)
                {
                    this._blogCategoryRepository = new GenericRepository<BlogCategory>(_context);
                }
                return this._blogCategoryRepository;
            }
        }

        public GenericRepository<Cart> CartRepository
        {
            get
            {
                if (this._cartRepository == null)
                {
                    this._cartRepository = new GenericRepository<Cart>(_context);
                }
                return this._cartRepository;
            }
        }

        public GenericRepository<CartItem> CartItemRepository
        {
            get
            {
                if (this._cartItemRepository == null)
                {
                    this._cartItemRepository = new GenericRepository<CartItem>(_context);
                }
                return this._cartItemRepository;
            }
        }

        public GenericRepository<Feedback> FeedbackRepository
        {
            get
            {
                if (this._feedbackRepository == null)
                {
                    this._feedbackRepository = new GenericRepository<Feedback>(_context);
                }
                return this._feedbackRepository;
            }
        }

        public GenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                if (this._orderDetailRepository == null)
                {
                    this._orderDetailRepository = new GenericRepository<OrderDetail>(_context);
                }
                return this._orderDetailRepository;
            }
        }

        public GenericRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (this._orderItemRepository == null)
                {
                    this._orderItemRepository = new GenericRepository<OrderItem>(_context);
                }
                return this._orderItemRepository;
            }
        }

        public GenericRepository<Payment> PaymentRepository
        {
            get
            {
                if (this._paymentRepository == null)
                {
                    this._paymentRepository = new GenericRepository<Payment>(_context);
                }
                return this._paymentRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_context);
                }
                return this._productRepository;
            }
        }

        public GenericRepository<ProductCategory> ProductCategoryRepository
        {
            get
            {
                if (this._productCategoryRepository == null)
                {
                    this._productCategoryRepository = new GenericRepository<ProductCategory>(_context);
                }
                return this._productCategoryRepository;
            }
        }

        public GenericRepository<ProductSubImage> ProductSubImageRepository
        {
            get
            {
                if (this._productSubImageRepository == null)
                {
                    this._productSubImageRepository = new GenericRepository<ProductSubImage>(_context);
                }
                return this._productSubImageRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_context);
                }
                return this._userRepository;
            }
        }

        public GenericRepository<UserAddress> UserAddressRepository
        {
            get
            {
                if (this._userAddressRepository == null)
                {
                    this._userAddressRepository = new GenericRepository<UserAddress>(_context);
                }
                return this._userAddressRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
