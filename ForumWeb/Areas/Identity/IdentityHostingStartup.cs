using System;
using ForumWeb.Areas.Identity.Data;
using ForumWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ForumWeb.Areas.Identity.IdentityHostingStartup))]
namespace ForumWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ForumWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ForumWebContextConnection")));

                services.AddDefaultIdentity<ForumWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ForumWebContext>();
            });
        }
    }
}