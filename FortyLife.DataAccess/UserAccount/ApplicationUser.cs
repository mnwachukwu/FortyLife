using System;
using System.Collections.Generic;

namespace FortyLife.DataAccess.UserAccount
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string Email { get; set; }
        
        public string ActivationKey { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string DisplayName { get; set; }

        public string AboutMe { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Collection> Collections { get; set; }
    }
}
