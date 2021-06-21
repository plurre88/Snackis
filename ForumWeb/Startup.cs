using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb
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
            services.AddHttpClient<Gateways.CategoryGateway>();
            services.AddHttpClient<Gateways.SubCategoryGateway>();
            services.AddHttpClient<Gateways.PostGateway>();
            services.AddHttpClient<Gateways.CommentGateway>();
            services.AddHttpClient<Gateways.ReportGateway>();
            services.AddHttpClient<Gateways.MessageGateway>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeUser",
                    policy => policy.RequireRole("User"));
                options.AddPolicy("MustBeAdmin",
                    policy => policy.RequireRole("Admin"));
            });
            services.AddRazorPages(options => 
            {
                options.Conventions.AuthorizePage("/Privacy", "MustBeUser");
                options.Conventions.AuthorizeFolder("/Admin", "MustBeAdmin");
                options.Conventions.AllowAnonymousToPage("/Admin/Info");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
