// execute as console application

using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

// CreateBuilder will ensure Kestrel is included and set up IIS integration
var builder = WebApplication.CreateBuilder(args);

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

// add a service - we bring in framework services that enable MVC in our app
builder.Services.AddControllersWithViews(); // ensure the app knows about ASP.NET Core MVC

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

var app = builder.Build();

// MapGet is a middleware components that listen incoming requests to the root application
//app.MapGet("/", () => "Hello World!");

// add middleware components
// bring in support for returning static files - to look for incoming request for static files (JPEG/CSS)
// it will find it in the wwwroot folder and return it
// it will then short circuit the request
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    // see errors inside the executing application (a diagnostic middleware component)
    app.UseDeveloperExceptionPage();

    // to know our environment, go to properties (right click on the project) > debug > general
}

// ability to be able to navigate to view (ensure ASP.NET Core able to handle incoming requests correctly)
app.MapDefaultControllerRoute();

app.Run();
