using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ManageUsers.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//using System.Web;


namespace ManageUsers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
            //_context = context;
        }

        public async Task Invoke(HttpContext httpContext, UserManager<WebApp1User> userManager, SignInManager<WebApp1User> signInManager, WebApp1Context db)
        {
           if(!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
             //   var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);
                var user = await db.WebApp1Users.AsNoTracking().FirstAsync(x => x.UserName == httpContext.User.Identity.Name);
                if(user == null)
                {
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/Identity/Account/Blocked");
                }else
                {
                    if(user.IsBanned == true && user.Id != null)
                    {
                        await signInManager.SignOutAsync();
                        httpContext.Response.Redirect("/Identity/Account/Blocked");
                    }
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
