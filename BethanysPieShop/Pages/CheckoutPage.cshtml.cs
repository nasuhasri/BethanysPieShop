using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages
{
    public class CheckoutPageModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        // constructor injection
        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [BindProperty]
        public Order Order { get; set; }

        // this will be executed when GET request is received
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // get access to data via Order property
            if (!ModelState.IsValid)
            {
                return Page(); // Razor PagesModel
            }

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first.");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(Order);
                _shoppingCart.ClearCart();
                
                return RedirectToAction("CheckoutCompletePage");
            }

            return Page();
        }
    }
}
