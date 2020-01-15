using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDemo.Areas.Identity.Data;
using WebDemo.Models;

[assembly: HostingStartup(typeof(WebDemo.Areas.Identity.IdentityHostingStartup))]
namespace WebDemo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AwesomeDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("WebDemoContextConnection")));

                services.AddDefaultIdentity<WebDemoUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AwesomeDbContext>();
            });
        }
    }
}