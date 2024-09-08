
using Maneger;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Runtime.Serialization;

namespace Day1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped();
            builder.Services.AddDbContext<MyDBContext>(i =>
            {
                i.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("connStr"));
            }
          );

            builder.Services.AddScoped<ProductManeger>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();


        }
    }
}