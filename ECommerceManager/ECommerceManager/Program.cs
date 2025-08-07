using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.Dtos.Product;
using ECommerceManager.�nterfaces;
using ECommerceManager.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Cookie ile kimlik do�rulama (kullan�c� oturumlar� i�in)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

// HttpClient'ler API adresiyle birlikte tan�mlan�yor
builder.Services.AddHttpClient<ICategoryManager, CategoryManager>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44321"); // API adresi
});
builder.Services.AddHttpClient<IProductManager, ProductManager>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44321");
});
builder.Services.AddHttpClient<IOrderManager, OrderManager>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44321");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
