using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace ManageUsers.Areas.Identity.Data
{
    [Keyless]
    public class WebApp1User : IdentityUser
    {
        [PersonalData]
        public DateTime FirstSignIn { get; set; }
        public DateTime LastSignIn { get; set; }
        public string SocialNetwork { get; set; }
        public Boolean IsBanned { get; set; }
        public string Name { get; set; }
    }
}
