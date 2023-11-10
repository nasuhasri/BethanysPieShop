namespace BethanysPieShop.Models;

public class OrderRepository : IOrderRepository
{
    private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
    private readonly IShoppingCart _shoppingCart;

    public OrderRepository(BethanysPieShopDbContext bethanysPieShopDbContext, IShoppingCart shoppingCart)
    {
        _bethanysPieShopDbContext = bethanysPieShopDbContext;
        _shoppingCart = shoppingCart;
    }

    public void CreateOrder(Order order)
    {
        // update OrderPlaced DateTime to current DateTime
        order.OrderPlaced = DateTime.Now;

        // find shopping cart items
        List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;

        // get shopping cart total
        order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

        // create order details
        order.OrderDetails = new List<OrderDetail>();

        // adding order with its details
        foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
        {
            // create new order detail for each shopping cart item
            var orderDetail = new OrderDetail
            {
                Amount = shoppingCartItem.Amount,
                PieId = shoppingCartItem.Pie.PieId,
                Price = shoppingCartItem.Pie.Price
            };

            order.OrderDetails.Add(orderDetail);
        }

        _bethanysPieShopDbContext.Orders.Add(order);

        _bethanysPieShopDbContext.SaveChanges();
    }
}
