using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageUsers.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ManageUsers.Pages
{
    public class UsersTableModel : PageModel
    {
        private readonly WebApp1Context _context;
        private readonly IConfiguration Configuration;

        private readonly UserManager<WebApp1User> _userManager;
        private readonly SignInManager<WebApp1User> _signInManager;
        public UsersTableModel(WebApp1Context context, IConfiguration configuration, SignInManager<WebApp1User> signInManager,
            UserManager<WebApp1User> userManager)
        {
            _context = context;
            Configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public List<WebApp1User> WebAppUsers { get; set; }
        public List<string> SocialNetworks { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<WebApp1User> webAppUsersIQ = from s in _context.WebApp1Users.AsNoTracking()
                                                    select s;
            WebAppUsers = webAppUsersIQ.AsNoTracking().ToList();
            SocialNetworks = _context.WebApp1Users.Select(u => u.SocialNetwork).Distinct().ToList();
        }

        public async Task OnPost(string submit, string[] AreChecked, string Reason)
        {
                
            IQueryable<WebApp1User> webAppUsersIQ = from s in _context.WebApp1Users.AsNoTracking()
                                                    select s;
            var user = await _context.WebApp1Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            if (submit == "Delete")
            {
                foreach (var item in webAppUsersIQ.AsNoTracking())
                {
                    if (AreChecked.Contains(item.Id.ToString()))
                    {
                         _context.WebApp1Users.Remove(item);
                        if (item.Id.ToString() == user.Id.ToString())
                        {
                            await _signInManager.SignOutAsync();
                        }
                    }
                }
            }
            if (submit == "Block")
            {
                foreach (var item in webAppUsersIQ.AsNoTracking())
                {
                    if (AreChecked.Contains(item.Id.ToString()))
                    {
                        item.IsBanned = true;
                        _context.WebApp1Users.Update(item);
                        if (item.Id.ToString() == user.Id.ToString())
                        {
                            await _signInManager.SignOutAsync();
                        }
                    }
                }
            }
            if (submit == "Unblock")
            {
                foreach (var item in webAppUsersIQ.AsNoTracking())
                {
                    if (AreChecked.Contains(item.Id.ToString()))
                    {
                        item.IsBanned = false;
                        _context.WebApp1Users.Update(item);
                    }
                }
            }
            await _context.SaveChangesAsync();
            WebAppUsers = webAppUsersIQ.AsNoTracking().ToList();
        }
    }
}
