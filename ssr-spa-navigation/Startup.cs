using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ssr_spa_navigation.Infrastructure;
using ssr_spa_navigation.Infrastructure.Repositories;

namespace ssr_spa_navigation
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

            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<DefaultStructureResult>();
            services.AddScoped<NoSidebarStructureResult>();
            services.AddScoped<LiveStoiximaStructureResult>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {

                MapRoutes(routes, name: "default_details",
                    template: "{controller=Home}/{ignoreme}-{id}",
                    defaults: new { action = "Details" },
                    constraints: new { id = @"\d+" });

                MapRoutes(routes, name: "league_details",
                    template: "{controller}/{ignore}-{sport:alpha}/{ignoreme}-{id}",
                    defaults: new { action = "Details" },
                    constraints: new { controller = "League", id = @"\d+" });

                MapRoutes(routes, name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: null,
                    constraints: null);

            });
        }

        public static IRouteBuilder MapRoutes(IRouteBuilder routes, string name, string template, object defaults,
            object constraints)
        {
            routes.MapRoute(
                name: name + "_api",
                template: "api/" + template,
                defaults: defaults,
                constraints: constraints,
                dataTokens: new { Name = "default_api" });

            routes.MapRoute(
                name: name,
                template: template,
                defaults: defaults,
                constraints: constraints);
            return routes;
        }
    }
}
