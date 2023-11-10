using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models;

public class BethanysPieShopDbContext : DbContext
{
    //add constructor - this is required
    public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options) : base(options)
    {

    }

    // exposed the entities, pie and category inside of the DBContext as a DbSet
    // behind the scene, they will be mapped to db tables
    // EF Core ensure entities get loaded from the db and we will be able to save it if necessary

    // set up the DbSet property
    // we've basically set to EF Core whereby these entities we want to match
    // 1) able to load data in these DbSets, 2) make changes to the data, 3) let the updated data flow back to the db when we want to save
    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
}
