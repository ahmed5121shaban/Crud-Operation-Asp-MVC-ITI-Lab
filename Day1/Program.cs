
using Day1.Helper;
using Maneger;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddSingleton<CloudinaryService>();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1004857600;
            });

            //builder.Services.AddScoped();
            builder.Services.AddDbContext<MyDBContext>(i =>
            {
                i.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("connStr"),
                b => b.MigrationsAssembly("Day1")
                );
            }
          );


            builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<MyDBContext>()
                .AddDefaultTokenProviders();

            //to add configuration in identity tables
            builder.Services.Configure<IdentityOptions>(o => o.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider);
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>,Claims>();
            builder.Services.AddScoped<ProductManeger>();
            builder.Services.AddScoped<AcountManager>();
            builder.Services.AddScoped<CartManager>();
            builder.Services.AddScoped<ManagerRole>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();


        }
    }
}