using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Centar.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fitness_Centar.Web.Areas.ModulClan.Helper;

namespace Fitness_Centar.Web
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

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(opts =>
            {
                opts.Cookie.IsEssential = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("lokalni")));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseCookiePolicy();

            app.UseSession();

            app.UseStaticFiles();

            app.UseSignalR(routes => 
            {
                routes.MapHub<LajkHub>("/ModulClan/Helper/LajkHub");
                routes.MapHub<KomentarHub>("/ModulClan/Helper/KomentarHub");
                routes.MapHub<FollowHub>("/ModulClan/Helper/FollowHub");
                routes.MapHub<NotificationHub>("/ModulClan/Helper/NotificationHub"); //Primjer
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
