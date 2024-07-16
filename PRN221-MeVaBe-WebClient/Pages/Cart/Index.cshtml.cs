using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<CartItem> CartItems { get; set; }
        public PRN221_MeVaBe_Repo.Models.Cart _Cart { get; set; }
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public int UserId { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public int CartItemId { get; set; }
        [BindProperty]
        public int totalCart { get; set; }
        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue)
            {
                return RedirectToPage("/HomePage");
            }

            if (unitOfWork.CartRepository.Get(filter: c => c.UserId == userId).Count() == 0)
            {
                _Cart = null;
                CartItems = default;
            }
            else
            {
                _Cart = unitOfWork.CartRepository.Get(filter: c => c.UserId == userId).FirstOrDefault();
                CartItems = unitOfWork.CartItemRepository.Get(filter: c => c.CartId == _Cart.Id, includeProperties: "Product").ToList();
            }
            return Page();
        }
        public JsonResult OnPostAddToCart(int productId, int userId, int quantity)
        {
            var cart = unitOfWork.CartRepository.Get(c => c.UserId == userId);
            if (cart.Count() == 0)
            {
                var cartId = 0;
                var product = unitOfWork.ProductRepository.GetByID(productId);
                var carts = unitOfWork.CartRepository.Get();
                if (carts.Count() == 0)
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
                if (quantity == 0)
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
                if (quantity == 0)
                {
                    _cart.Total += product.Price;
                }
                else
                {
                    _cart.Total += (product.Price * quantity);
                }
                unitOfWork.CartRepository.Update(_cart);
                unitOfWork.Save();
                var cartItem = unitOfWork.CartItemRepository.Get(c => c.ProductId == productId && c.CartId == _cart.Id);
                if (cartItem.Count() == 0)
                {
                    var cartItems = unitOfWork.CartItemRepository.Get();
                    CartItem newCartItem = null;
                    if (quantity == 0)
                    {
                        if (cartItems.Count() > 0)
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
                                Id = 1,
                                CartId = _cart.Id,
                                ProductId = productId,
                                Quanity = 1
                            };
                        }

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
            return new JsonResult(new { success = true });
        }
        public JsonResult OnPostUpdateCartQuantity(int userId)
        {
            var cart = unitOfWork.CartRepository.Get(filter: c => c.UserId == userId).First();
            var cartItem = unitOfWork.CartItemRepository.Get(filter: c => c.CartId == cart.Id);

            return new JsonResult(new { success = true, data = cartItem.Count() });
        }
        public JsonResult OnPostDeleteCartItems(int cartItemId)
        {
            var cartItems = unitOfWork.CartItemRepository.GetByID(cartItemId);
            var product = unitOfWork.ProductRepository.GetByID(cartItems.ProductId);
            var cart = unitOfWork.CartRepository.GetByID(cartItems.CartId);
            cart.Total -= (product.Price * cartItems.Quanity);
            unitOfWork.CartRepository.Update(cart);
            unitOfWork.Save();
            unitOfWork.CartItemRepository.Delete(cartItems);
            unitOfWork.Save();
            return new JsonResult(new { success = true });
        }
        public JsonResult OnPostUpdateCartItem(int cartItemId, int quantity, int totalCart)
        {
            var cartItem = unitOfWork.CartItemRepository.GetByID(cartItemId);
            cartItem.Quanity = quantity;

            unitOfWork.CartItemRepository.Update(cartItem);
            unitOfWork.Save();

            var cart = unitOfWork.CartRepository.GetByID(cartItem.CartId);
            cart.Total = totalCart;

            unitOfWork.CartRepository.Update(cart);
            unitOfWork.Save();

            return new JsonResult(new { success = true });
        }
    }
}
