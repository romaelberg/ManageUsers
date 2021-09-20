using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ManageUsers.Areas.Identity.Data;

namespace ManageUsers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApp1Context _context;
        public IndexModel(WebApp1Context context)
        {
            _context = context;
        }
        public int FacebookUsersCount { get; set; }
        public int GoogleUsersCount { get; set; }
        public void OnGet()
        {
            FacebookUsersCount = _context.WebApp1Users.Count(t => t.SocialNetwork == "Facebook");
            GoogleUsersCount = _context.WebApp1Users.Count(t => t.SocialNetwork == "Google");
        }
    }
}
