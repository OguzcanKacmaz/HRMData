using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using HRMData.WEB.Localizations;
using Microsoft.AspNetCore.Identity;

namespace HRMData.WEB.Entensions
{
    public static class StartupExtansion
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2); // oluşturduğumuz şifre sıfırlama tokenının ömrünü 2 saat olarak ayarladık.
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;//email adresinin unique olup olmadığını belirlediğimiz yer
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyz1234567890_@.";//izin verilen username karakterleri belirttiğimiz yer
                options.Password.RequiredLength = 6; // şifre min 6 karakter 
                options.Password.RequireNonAlphanumeric = true; //alphanumerik karakter olsun mu?(* ? vb..)
                options.Password.RequireLowercase = true; // küçük karakter zorunlu olsun mu?
                options.Password.RequireUppercase = true; //büyük karakter zorunlu olsun mu?
                options.Password.RequireDigit = true; //Sayı Zorunlu olsun mu?

                options.Lockout.MaxFailedAccessAttempts = 3; // 3 yanlış girişte kitler
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //3 yanlış giriş sonrası 3 dk kitler   
            })
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";
            });
        }
    }
}
