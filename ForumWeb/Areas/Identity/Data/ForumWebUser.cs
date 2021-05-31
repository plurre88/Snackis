using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ForumWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ForumWebUser class
    public class ForumWebUser : IdentityUser
    {
        public string NickName { get; set; }
        public string Img { get; set; }
    }
}
