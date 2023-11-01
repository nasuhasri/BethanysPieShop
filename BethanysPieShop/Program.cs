// execute as console application

// CreateBuilder will ensure Kestrel is included and set up IIS integration
var builder = WebApplication.CreateBuilder(args);

// add a service - we bring in framework services that enable MVC in our app
builder.Services.AddControllersWithViews(); // ensure the app knows about ASP.NET Core MVC

var app = builder.Build();

// MapGet is a middleware components that listen incoming requests to the root application
app.MapGet("/", () => "Hello World!");

app.Run();
