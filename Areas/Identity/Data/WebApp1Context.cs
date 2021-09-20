using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManageUsers.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ManageUsers.Areas.Identity.Data
{
    public class WebApp1Context: IdentityDbContext<WebApp1User>
    {
        public WebApp1Context(DbContextOptions<WebApp1Context> options)
           : base(options)
        {
        }

        public DbSet<WebApp1User> WebApp1Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<WebApp1User>().ToTable(nameof(WebApp1User));
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
