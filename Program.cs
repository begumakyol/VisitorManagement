using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Business;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Authentication ve Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/GirisYap";  // Giriþ sayfasý yolu
        options.LogoutPath = "/User/CikisYap"; // Çýkýþ sayfasý
        options.AccessDeniedPath = "/Home/Yetkisiz"; // Yetkisiz eriþim yönlendirmesi
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Veritabaný baðlantýsý
builder.Services.AddDbContext<VisitorDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisler
builder.Services.AddScoped<ILocationManager, LocationManager>();
builder.Services.AddScoped<IVisitorManager, VisitorManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

// Controller'lar ve görünümler
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // JSON'daki özellik adlarýný deðiþtirmemek için
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware'ler
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
 

// Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Varsayýlan yönlendirme (Giriþ yapmayanlarý yönlendir)
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/User/GirisYap");
        return;
    }
    await next();
});

// Controller rotalarý
app.MapControllerRoute(
    name: "default",
    pattern: "User/GirisYap", 
    defaults: new { controller = "User", action = "GirisYap" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

