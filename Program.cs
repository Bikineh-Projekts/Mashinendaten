using MaschinenDataein.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ------------------ Services ------------------

// MVC (+ Razor Runtime Compilation nur in DEBUG)
#if DEBUG
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
#else
builder.Services.AddControllersWithViews();
#endif

// EF Core (SQL Server)
builder.Services.AddDbContext<MaschinenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.Name = ".Portal.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // nur über HTTPS
});

// Upload-Limits (große Dateien)
builder.WebHost.ConfigureKestrel(o =>
{
    // null => kein Limit (empfohlen statt long.MaxValue)
    o.Limits.MaxRequestBodySize = null;
});
builder.Services.Configure<FormOptions>(o =>
{
    o.MultipartBodyLengthLimit = long.MaxValue;
});
builder.Services.Configure<IISServerOptions>(o =>
{
    o.MaxRequestBodySize = int.MaxValue;
});

var app = builder.Build();

// ------------------ Middleware Pipeline ------------------

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Statische Dateien (optional: einfach ohne benutzerdefiniertes Caching)
app.UseStaticFiles();

app.UseRouting();

// Session NACH UseRouting, VOR MapControllerRoute
app.UseSession();

// (Falls später Auth kommt)
// app.UseAuthentication();
app.UseAuthorization();

// Custom Fehlerseiten für Statuscodes wie 404, 500 usw.
app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

// ------------------ Routing ------------------
// Standardroute: /Planung/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ------------------ Start ------------------
app.Run();
