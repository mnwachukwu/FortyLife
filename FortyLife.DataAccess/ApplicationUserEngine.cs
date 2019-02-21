using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.DataAccess
{
    public static class ApplicationUserEngine
    {
        private static ObjectCache ApplicationUserCache { get; set; }

        public static void Initialize()
        {
            ApplicationUserCache = new MemoryCache("ApplicationUserCache");
        }

        public static bool CreateAccount(string email, string password)
        {
            if (GetApplicationUser(email) != null)
            {
                return false;
            }

            var salt = UserAuthenticator.GetHashString(DateTime.Now.Ticks.ToString());
            var passwordHash = UserAuthenticator.ComputeHash(password, salt);

            var newUser = new ApplicationUser
            {
                Email = email,
                DisplayName = email.Split('@')[0],
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            using (var db = new FortyLifeDbContext())
            {
                try
                {
                    db.ApplicationUsers.AddOrUpdate(newUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    // TODO: implement proper logger for exceptions
                    return false;
                }
            }
        }

        public static ApplicationUser GetApplicationUser(string email)
        {
            if (ApplicationUserCache.Contains(email))
            {
                return (ApplicationUser)ApplicationUserCache[email];
            }

            using (var db = new FortyLifeDbContext())
            {
                var user = db.ApplicationUsers.FirstOrDefault(i => i.Email == email);

                if (user != null)
                {
                    ApplicationUserCache.Set(email, user, DateTimeOffset.Now.AddDays(7));
                    return user;
                }
            }

            return null;
        }
    }
}
