using Common.Models;
using Lab1_Data;
using Lab1_Web.Abstraction;
using Lab1_Web.Middlewares;
using Lab1_Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    return new SqlConnectionFactory(configuration);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();

// app.UseUserEntityEndpoints();
// app.UseArticleEntityEndpoints();
// app.UseCommentEntityEndpoints();

app.Run();