// execute as console application

using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

// CreateBuilder will ensure Kestrel is included and set up IIS integration
var builder = WebApplication.CreateBuilder(args);

// NOTES: order for services doent matter, only in the middleware the order is matter

/*
 * when using repositories, use AddScoped -> will create a singleton while request is being handled.
 * -- builder.Services.AddScoped
 * will create a new instance every time, over and over again
 * -- builder.Services.AddTransient
 * will create just one single instance and keep that around
 * -- builder.Services.AddSingleton
*/

/*
 * we are not using mock anymore. will be using the PieRepository and CategoryRepository
*/
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// add session service for shopping cart and passing the service provider
// AddScoped - going to create a ShoppingCart for the request, so all the places with the request that have access to the shopping cart will use that same shopping cart that gets instantiated in the GetCart method.
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// add a service - we bring in framework services that enable MVC in our app
// this is needed bcs we want both controllers and views
// ensure the app knows about ASP.NET Core MVC
builder.Services.AddControllersWithViews()
    // ignore loop where pie reference to category and category reference to list of pies
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// enable razor pages
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

// enable blazor app
builder.Services.AddServerSideBlazor();

// add support for controllers which is required to build an API
// if only build API, just use AddControllers
// but since, we have AddControllersWithViews(), we dont need AddControllers()
//builder.Services.AddControllers();

var app = builder.Build();

// MapGet is a middleware components that listen incoming requests to the root application
//app.MapGet("/", () => "Hello World!");

// add middleware components
// bring in support for returning static files - to look for incoming request for static files (JPEG/CSS)
// it will find it in the wwwroot folder and return it
// it will then short circuit the request
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    // see errors inside the executing application (a diagnostic middleware component)
    app.UseDeveloperExceptionPage();

    // to know our environment, go to properties (right click on the project) > debug > general
}

/*
 * ability to be able to navigate to view (ensure ASP.NET Core able to handle incoming requests correctly)
 * add support for routing to controller and controller actions
 * DefaultControllerRoute is "{controller=Home}/{action=Index}/{id?}"
 * it will match with a pattern like above
 * if we dont specify the controller and action, default Home and Index will be used
*/

app.MapDefaultControllerRoute();

// another way of using route but need to do it multiple times
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// brings in correct middleware support for razor pages
// enables Razor PageModel
app.MapRazorPages();

// bring in support for routing required to build api
//app.MapControllers();

// enabled Blazor in the pipeline
app.MapBlazorHub();
// everything that arrives on /app/whatever will be handles by /app/index -> page that hosts blazor applicatio
app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

DbInitializer.Seed(app); // app here means applicationBuilder

app.Run();
