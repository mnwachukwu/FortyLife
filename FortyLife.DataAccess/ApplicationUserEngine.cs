using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
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
                PasswordSalt = salt,
                CreateDate = DateTime.Now,
                ActivationKey = RemoveReservedUnsafeCharacters(UserAuthenticator.GetHashString(DateTime.Now.ToString("G")))
            };

            using (var db = new FortyLifeDbContext())
            {
                try
                {
                    db.ApplicationUsers.AddOrUpdate(newUser);
                    db.SaveChanges();

                    SendActivationEmail(email);

                    return true;
                }
                catch (Exception e)
                {
                    // TODO: implement proper logger for exceptions
                    return false;
                }
            }
        }

        private static string RemoveReservedUnsafeCharacters(string text)
        {
            // UGLY way to remove reserved and unsafe characters from values (so it can be passed via url)
            return text
                .Replace("/", string.Empty)
                .Replace("\\", string.Empty)
                .Replace("?", string.Empty)
                .Replace("&", string.Empty)
                .Replace("\"", string.Empty)
                .Replace("<", string.Empty)
                .Replace(">", string.Empty)
                .Replace(";", string.Empty)
                .Replace(":", string.Empty)
                .Replace("@", string.Empty)
                .Replace("=", string.Empty)
                .Replace("#", string.Empty)
                .Replace("%", string.Empty)
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .Replace("|", string.Empty)
                .Replace("^", string.Empty)
                .Replace("~", string.Empty)
                .Replace("[", string.Empty)
                .Replace("]", string.Empty);
        }

        public static void SendActivationEmail(string email)
        {
            // Compose and send an activation email - account creation success!
            var body = File.ReadAllText(HttpRuntime.AppDomainAppPath + "/App_Data/ActivationEmail.html");
            var user = GetApplicationUser(email);

            if (user != null)
            {
                body = body.Replace("(name)", $"{user.DisplayName}#{user.Id}");
                body = body.Replace("(code)", user.ActivationKey);
                body = body.Replace("(activation link w/ code)",
                    $"http://forty.life/Account/Activate/{user.Email}/{user.ActivationKey}");
                body = body.Replace("(activationlink)", "http://forty.life/Account/Activate");

                new Mailer().SendMail(user.Email, "Welcome to Forty Life!", body);
            }
        }

        public static void ResendActivationEmail(string email)
        {
            var user = GetApplicationUser(email);
            if (user != null)
            {
                RegenerateActivationKey(email);
                UpdateUserInCache(email);
                SendActivationEmail(email);
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

        public static void UpdateUserInCache(string email)
        {
            if (ApplicationUserCache.Contains(email))
            {
                ApplicationUserCache.Remove(email);
                GetApplicationUser(email);
            }
        }

        public static void ActivateUser(string email)
        {
            using (var db = new FortyLifeDbContext())
            {
                var user = db.ApplicationUsers.FirstOrDefault(i => i.Email == email);
                if (user != null)
                {
                    user.ActivationKey = null;
                    db.SaveChanges();
                }
            }
        }

        private static void RegenerateActivationKey(string email)
        {
            using (var db = new FortyLifeDbContext())
            {
                var user = db.ApplicationUsers.FirstOrDefault(i => i.Email == email);
                if (user != null)
                {
                    user.ActivationKey = RemoveReservedUnsafeCharacters(UserAuthenticator.GetHashString(DateTime.Now.ToString("G")));
                    db.SaveChanges();
                }
            }
        }
    }
}
