using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ManageUsers.Areas.Identity.Data;

namespace ManageUsers
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
            services.AddDbContext<ManageUsersIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthContextConnection")));
           // services.AddScoped<AuthMiddleware>();
            services.AddAuthentication()
        .AddFacebook(options => {
            options.AppId = "446854986600730";
            options.AppSecret = "0add90d0b85d0a8790a585d8b26c2c8c";
        })
        .AddVkontakte(options =>
        {
            options.ClientId = "7956899";
            options.ClientSecret = "05f5as7IyoKJFTP0Ikq0";
            options.Scope.Add("email");
        })
        .AddGoogle(options =>
        {
            options.ClientId = "43531528851-l7e0kkjh09hec1b97c27ck7p5m44dfns.apps.googleusercontent.com";
            options.ClientSecret = "aPtS7J_WdpqTgKJhNCJiO9A9";
            options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
            options.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
            options.SaveTokens = true;
            options.Events.OnCreatingTicket = ctx =>
            {
                List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

                tokens.Add(new AuthenticationToken()
                {
                    Name = "TicketCreated",
                    Value = DateTime.UtcNow.ToString()
                });

                ctx.Properties.StoreTokens(tokens);

                return Task.CompletedTask;
            };
        });

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Account/Manage");
                options.Conventions.AuthorizePage("/Account/Logout");
                options.Conventions.AuthorizePage("/UsersTable");
                options.Conventions.AuthorizePage("/MyClaims");
            });
            services.AddControllersWithViews();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<WebApp1Context>().Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
