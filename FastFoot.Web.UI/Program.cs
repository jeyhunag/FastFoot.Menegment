using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.HelperExtensions.IdentityExtensions;
using FastFoot.Web.UI.Provider;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Boolean 
            builder.Services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Identity AppRole,AppUser Security 
            builder.Services.AddIdentityServices();

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
            app.MapAreaControllerRoute("FoltAdminArea",
            areaName: "FoltAdmin",
            pattern: "Foltadmin/{controller=Home}/{action=index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Homes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}