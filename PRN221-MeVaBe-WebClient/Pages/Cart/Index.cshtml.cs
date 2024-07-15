using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
       
        public JsonResult OnGet(int productId, int userId, int quantity)
        {
            if(quantity == 0)
            {
                addToCart(productId, userId, 0);
            }
            else
            {
                addToCart(productId, userId, quantity);
            }
            return new JsonResult(new { success = true });
        }
        public void addToCart(int productId, int userId, int quantity)
        {
            var cart = unitOfWork.CartRepository.Get(c => c.UserId == userId);
            if (cart.Count() == 0)
            {
                var cartId = 0;
                var product = unitOfWork.ProductRepository.GetByID(productId);
                var carts = unitOfWork.CartRepository.Get();
                if(carts.Count() == 0)
                {
                    cartId = 1;
                }
                else
                {
                    cartId = carts.LastOrDefault().Id + 1;
                }
                PRN221_MeVaBe_Repo.Models.Cart _cart = new PRN221_MeVaBe_Repo.Models.Cart();
                _cart.Id = cartId;
                _cart.UserId = userId;
                if(quantity == 0)
                {
                    _cart.Total = product.Price;
                }
                else
                {
                    _cart.Total = product.Price * quantity;
                }

                unitOfWork.CartRepository.Insert(_cart);
                unitOfWork.Save();
                var cartItems = unitOfWork.CartItemRepository.Get();
                int cartItemId = 1;
                if (cartItems.Count() > 0)
                {
                    cartItemId = cartItems.LastOrDefault().Id + 1;
                }
                CartItem cartItem = null;
                if (quantity == 0)
                {
                    cartItem = new CartItem
                    {
                        Id = cartItemId,
                        CartId = cartId,
                        ProductId = productId,
                        Quanity = 1
                    };
                }
                else
                {
                    cartItem = new CartItem
                    {
                        Id = cartItemId,
                        CartId = cartId,
                        ProductId = productId,
                        Quanity = quantity
                    };
                }
                
                unitOfWork.CartItemRepository.Insert(cartItem);
                unitOfWork.Save();
            }
            else
            {
                var _cart = cart.FirstOrDefault();
                var product = unitOfWork.ProductRepository.GetByID(productId);
                _cart.Total += product.Price;
                unitOfWork.CartRepository.Update(_cart);
                unitOfWork.Save();
                var cartItem = unitOfWork.CartItemRepository.Get(c => c.ProductId == productId && c.CartId == _cart.Id);
                if(cartItem.Count() == 0)
                {
                    var cartItems = unitOfWork.CartItemRepository.Get();
                    CartItem newCartItem = null;
                    if (quantity == 0)
                    {
                        newCartItem = new CartItem
                        {
                            Id = cartItems.LastOrDefault().Id + 1,
                            CartId = _cart.Id,
                            ProductId = productId,
                            Quanity = 1
                        };
                    }
                    else
                    {
                        newCartItem = new CartItem
                        {
                            Id = cartItems.LastOrDefault().Id + 1,
                            CartId = _cart.Id,
                            ProductId = productId,
                            Quanity = quantity
                        };
                    }
                   
                    unitOfWork.CartItemRepository.Insert(newCartItem);
                    unitOfWork.Save();
                }
                else
                {
                    var existCartItem = cartItem.FirstOrDefault();
                    if (quantity == 0)
                    {
                        existCartItem.Quanity += 1;
                    }
                    else
                    {
                        existCartItem.Quanity += quantity;
                    }
                    unitOfWork.CartItemRepository.Update(existCartItem);
                    unitOfWork.Save();
                }
            }
        }
    }
}
