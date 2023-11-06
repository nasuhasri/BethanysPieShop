// execute as console application

using BethanysPieShop.Models;

// CreateBuilder will ensure Kestrel is included and set up IIS integration
var builder = WebApplication.CreateBuilder(args);

// add a service - we bring in framework services that enable MVC in our app
builder.Services.AddControllersWithViews(); // ensure the app knows about ASP.NET Core MVC

/*
 * when using repositories, use AddScoped -> will create a singleton while request is being handled.
 * -- builder.Services.AddScoped
 * will create a new instance every time, over and over again
 * -- builder.Services.AddTransient
 * will create just one single instance and keep that around
 * -- builder.Services.AddSingleton
*/
builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();

var app = builder.Build();

// MapGet is a middleware components that listen incoming requests to the root application
//app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment())
{
    // see errors inside the executing application (a diagnostic middleware component)
    app.UseDeveloperExceptionPage();

    // to know our environment, go to properties (right click on the project) > debug > general
}

// add middleware components
// bring in support for returning static files - to look for incoming request for static files (JPEG/CSS)
// it will find it in the wwwroot folder and return it
// it will then short circuit the request
app.UseStaticFiles();

// ability to be able to navigate to view (ensure ASP.NET Core able to handle incoming requests correctly)
app.MapDefaultControllerRoute();

app.Run();
