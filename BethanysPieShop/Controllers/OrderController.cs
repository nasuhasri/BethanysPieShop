using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCart _shoppingCart;

    public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
    {
        _orderRepository = orderRepository;
        _shoppingCart = shoppingCart;
    }

    // if we dont specify anything, GET request assumed
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost] // POST method
    public IActionResult Checkout(Order order)
    {
        // check if there is items in their shopping cart
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        /*
         * ModelState is like a side product of model binding. When model binding is happening, any errors occurred will appear in this ModelState
         */
        if (_shoppingCart.ShoppingCartItems.Count() == 0)
        {
            ModelState.AddModelError("", "Your cart is empty, add some pies first.");
        }

        if (ModelState.IsValid)
        {
            _orderRepository.CreateOrder(order);
            _shoppingCart.ClearCart();

            // redirect the user to another view
            // redirect user to another action
            return RedirectToAction("CheckoutComplete");
        }

        // if ModelState not valid, it will return the same view which is checkout view and passing in an order that will enable the view to repopulate the fields
        return View(order);
    }

    public IActionResult CheckoutComplete()
    {
        ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies.";

        return View();
    }
}
