using CoreIdentity.Data;
using CoreIdentity.Models.IdentityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

// ApplicationDbContext burada servis olarak eklenmiş, eğer contextten bir instance almak istersek constructordan bunu inject etmemiz lazım. Yani dependency injection işlemi yapmamız lazım.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));   // ConnectionString in adı yani mvc de yazdığımız add name:MyCon daki MyCon u temsil ediyor.Bu ismi değiştirebiliriz ama aynı zamanda appsetting içerisinden de değiştirmemiz lazım.
                     
            // Hem kullanıcı giriş çıkışlarını yönetebilmek hem de role ile ilgili işlem yapabilmek için bu şekilde yazmalıyız.
            services.AddIdentity<ApplicationUser,ApplicationRole>()  // Burada IdentityUser yazıyordu fakat biz onu biraz geliştirmek için ApplicationUser sınıfını  yazdık ve IdentitUserdan kalıtım aldırdık. Authorize işlemleri içinde role yönetimine ihtiyaç duyduk bunun içinde AddDefaultIdentity yerine AddIdentity yazdık ve ApplicationRole ude ekledik. Bu sınıfta IdentityRole den kalıtım aldı.

                //.AddRoles<ApplicationRole>()    // Role yonetimi için de buraya servis olarak role sınıfını ekledik ve yine onu da IdentityRole dan kalıtım aldırdık.
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // KullanıcıAdı, Şifre, Yanlış Giriş ve Cookie ayarları
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true; // İçerisinde rakam olmalı mı
                options.Password.RequireLowercase = false; // İçerisinde küçük harf olmalı mı
                options.Password.RequireNonAlphanumeric = true; // Özel bir karakter olmalı mı &,-,_ gibi
                options.Password.RequireUppercase = true;   // İçerisinde büyük harf olmalı mı
                options.Password.RequiredLength = 6; // En az 6 karakter
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);  // Şifreyi aşağıda belirtilen sayıda yanlış girdikten sonra ne kadar süre ile sistemden uzaklaştırılacağını beliritiyor.
                options.Lockout.MaxFailedAccessAttempts = 3; // Maksimum yanlış girme saysısı.
                options.Lockout.AllowedForNewUsers = false; // Yanlış giren kişiye tekrar kullanıcı oluşturma izni verip vermeme ayarı

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";  // UserName için kullanılabilecek karakterler.
                options.User.RequireUniqueEmail = true; // Kullanıcı emaillerinin unique olma ayarı
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Uygulamaya bağlandığımızdaki cookie süresi

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();    // Authentication istiyorsak bu kodu core projelerimize eklemeliyiz.

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();
        }
    }
}