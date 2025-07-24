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
        options.LoginPath = "/User/GirisYap";  // Giri� sayfas� yolu
        options.LogoutPath = "/User/CikisYap"; // ��k�� sayfas�
        options.AccessDeniedPath = "/Home/Yetkisiz"; // Yetkisiz eri�im y�nlendirmesi
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Veritaban� ba�lant�s�
builder.Services.AddDbContext<VisitorDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisler
builder.Services.AddScoped<ILocationManager, LocationManager>();
builder.Services.AddScoped<IVisitorManager, VisitorManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

// Controller'lar ve g�r�n�mler
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // JSON'daki �zellik adlar�n� de�i�tirmemek i�in
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

// Varsay�lan y�nlendirme (Giri� yapmayanlar� y�nlendir)
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/User/GirisYap");
        return;
    }
    await next();
});

// Controller rotalar�
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

