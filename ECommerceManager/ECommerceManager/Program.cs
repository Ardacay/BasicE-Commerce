using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.Dtos.Product;
using ECommerceManager.Ýnterfaces;
using ECommerceManager.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ICategoryManager, CategoryManager>();
builder.Services.AddHttpClient<IProductManager, ProductManager>();
builder.Services.AddHttpClient<IOrderManager, OrderManager>();

//builder.Services.AddScoped<ICategoryManager, CategoryManager>();
//builder.Services.AddScoped<IProductManager, ProductManager>();
//builder.Services.AddScoped<IOrderManager, OrderManager>();

builder.Services.AddHttpClient<ICategoryManager, CategoryManager>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44321"); 
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
