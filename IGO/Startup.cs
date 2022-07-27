using IGO.Data;
using IGO.Hubs;
using IGO.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Unicode;
using System.Threading.Tasks;



namespace IGO
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DemoIgoContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IGOConnection")));
            services.AddDbContext<DemoIgoContext>(option =>
            option.UseLazyLoadingProxies().UseSqlServer("IGOConnection"));

            services.AddRazorPages().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);


            });
            services.AddControllersWithViews().AddNewtonsoftJson
               (options =>
               {

                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });
            services.AddRazorPages().AddNewtonsoftJson(options =>
           {
               options.UseMemberCasing();

           });

            services.AddDatabaseDeveloperPageExceptionFilter();
<<<<<<< HEAD

            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });
            services.AddRazorPages().AddNewtonsoftJson(options =>
            {
                options.UseMemberCasing();
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddRazorPages().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);

            });

            services.AddSession();

=======
           
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddSession();  //¥[¤JsessionªA°È
            services.AddSignalR();
>>>>>>> pr/19
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
<<<<<<< HEAD

            app.UseSession();  //Â±Ã’Â¥ÃŽsessionÂªAÂ°Ãˆ
=======
          
            app.UseSession();  //±Ò¥ÎsessionªA°È
>>>>>>> pr/19

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
<<<<<<< HEAD
                    pattern: "{area:exists}/{controller=Order}/{action=List}/{id?}");
=======
                    pattern: "{area:exists}/{controller=Home}/{action=List}/{id?}");
>>>>>>> pr/19
                endpoints.MapControllerRoute(
                    name: "default",

                    pattern: "{controller=ShoppingCart}/{action=List}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
