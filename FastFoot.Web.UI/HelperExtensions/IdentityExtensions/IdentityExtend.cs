using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Identity;

namespace FastFoot.Web.UI.HelperExtensions.IdentityExtensions
{
    public static class IdentityExtend
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opts =>
            {

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            return services;
        }
    }
}
