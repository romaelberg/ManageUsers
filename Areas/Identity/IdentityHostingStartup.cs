using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ManageUsers.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;
using ManageUsers.Areas.Identity.Data;

[assembly: HostingStartup(typeof(ManageUsers.Areas.Identity.IdentityHostingStartup))]
namespace ManageUsers.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebApp1Context>(options =>
                     options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<WebApp1User>()
                    .AddEntityFrameworkStores<WebApp1Context>();
            });
        }
    }
}