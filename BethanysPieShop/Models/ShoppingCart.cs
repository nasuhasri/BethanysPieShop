using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models;

public class ShoppingCart : IShoppingCart
{
    private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    private ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
    {
        _bethanysPieShopDbContext = bethanysPieShopDbContext;
    }

    /*
     * when user visit the site, this code going to run.
     * check if there's ID called CartId available for that user.
     * if not, create a new GUID and restore as CartId.
     * when user returning, find existing CartId.
     */
    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        // return fully created ShoppingCart
        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Pie pie)
    {
        var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };

            _bethanysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }

        _bethanysPieShopDbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                // if its the last item, we remove the item altogether
                _bethanysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _bethanysPieShopDbContext.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        // return the shopping cart items for the given shopping cart id including pies
        return ShoppingCartItems ??= _bethanysPieShopDbContext.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Include(s => s.Pie)
            .ToList();
    }

    public void ClearCart()
    {
        // remove all the shopping cart items for the given shopping cart id
        var cartItems = _bethanysPieShopDbContext.ShoppingCartItems
            .Where(cart => cart.ShoppingCartId == ShoppingCartId);

        _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);
        _bethanysPieShopDbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = _bethanysPieShopDbContext.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Pie.Price * c.Amount).Sum();

        return total;
    }

    
}
