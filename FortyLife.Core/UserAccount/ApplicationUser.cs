using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Core.UserAccount
{
    public class ApplicationUser
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string DisplayName { get; set; }

        public string AboutMe { get; set; }

        public List<Collection> Collections { get; set; }
    }
}
