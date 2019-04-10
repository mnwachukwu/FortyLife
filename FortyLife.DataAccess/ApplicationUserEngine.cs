using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using FortyLife.DataAccess.TCGPlayer;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.DataAccess
{
    public static class ApplicationUserEngine
    {
        public static string[] ReservedAndUnsafeCharacters =
            {"/", "\\", "?", "&", "\"", "<", ">", ";", ":", "@", "=", "#", "%", "{", "}", "|", "^", "~", "[", "]"};

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

        public static void DeleteAllCardsInCollection(int collectionId)
        {
            using (var db = new FortyLifeDbContext())
            {
                foreach (var card in db.CollectionCards.Where(i => i.CollectionId == collectionId))
                {
                    db.CollectionCards.Remove(card);
                }

                db.SaveChanges();
            }
        }

        public static void DeleteUserCollection(string email, Collection collection, out string error)
        {
            error = "";

            using (var db = new FortyLifeDbContext())
            {
                var users = db.ApplicationUsers.Include(i => i.Collections.Select(j => j.Cards));
                var user = users.FirstOrDefault(i => i.Email == email);

                if (user != null)
                {
                    if (user.Collections.Exists(i => i.CollectionId == collection.CollectionId))
                    {
                        // remove it from the object in memory
                        user.Collections.RemoveAll(i => i.CollectionId == collection.CollectionId);

                        // remove all cards associated with the collection in the db
                        foreach (var card in db.CollectionCards.Where(i => i.CollectionId == collection.CollectionId))
                        {
                            db.CollectionCards.Remove(card);
                        }

                        // remove the collection from the db
                        db.Collections.Remove(db.Collections.First(i => i.CollectionId == collection.CollectionId));

                        db.SaveChanges();
                    }
                    else
                    {
                        error = "Error! Collection doesn't exist.";
                    }
                }
                else
                {
                    error = "Error! No known user exists with an email address matching the current session claim.";
                }
            }
        }

        public static void AddOrUpdateUserCollection(string email, Collection collection, out string error)
        {
            error = "";

            using (var db = new FortyLifeDbContext())
            {
                var users = db.ApplicationUsers.Include(i => i.Collections.Select(j => j.Cards));
                var user = users.FirstOrDefault(i => i.Email == email);

                if (user != null)
                {
                    if (user.Collections == null)
                        user.Collections = new List<Collection>();

                    var collectionToUpdate =
                        user.Collections.FirstOrDefault(i => i.CollectionId == collection.CollectionId);

                    if (collectionToUpdate != null)
                    {
                        collectionToUpdate.CreateDate = collection.CreateDate;
                        collectionToUpdate.LastEditDate = collection.LastEditDate;
                        collectionToUpdate.Name = collection.Name;
                        collectionToUpdate.Description = collection.Description;
                    }
                    else
                    {
                        collection.Cards = null;
                        user.Collections.Add(collection);
                    }

                    db.SaveChanges();
                }
                else
                {
                    error = "Error! No known user exists with an email address matching the current session claim.";
                }
            }
        }

        public static void AddOrUpdateUserCollectionCard(int collectionId, IEnumerable<CollectionCard> cards, out string error)
        {
            error = "";

            using (var db = new FortyLifeDbContext())
            {
                var scryfallRequestEngine = new ScryfallRequestEngine();
                var collectionCards = db.CollectionCards;
                var cardList = cards.ToList();

                // fill in set codes for entries that don't have one
                foreach (var card in cardList)
                {
                    if (string.IsNullOrEmpty(card.SetCode))
                    {
                        card.SetCode = scryfallRequestEngine.CardSearchRequest(card.Name).Data.FirstOrDefault()?.Set.ToUpper();
                    }
                }

                var duplicates = cardList.GroupBy(i => new { i.Name, i.SetCode, i.Foil })
                    .Where(i => i.Count() > 1)
                    .Select(i => i.Key);
                var duplicateList = duplicates.ToList();

                if (!duplicateList.Any())
                {
                    DeleteAllCardsInCollection(collectionId);

                    foreach (var card in cardList)
                    {
                        card.CollectionId = collectionId;

                        collectionCards.AddOrUpdate(card);
                    }

                    db.SaveChanges();
                }
                else
                {
                    var errorCard = duplicateList.First();
                    var errorCardName = errorCard.Name;

                    // find the second occurence of any duplicated item
                    var firstOcc = cardList.FindIndex(i => i.Name == errorCard.Name && i.SetCode == errorCard.SetCode) + 1;
                    var errorLine = cardList.FindIndex(firstOcc,
                        i => i.Name == errorCard.Name && i.SetCode == errorCard.SetCode) + 1;

                    error = $"Duplicate card detected: {errorCardName}. (Lines {firstOcc} and {errorLine})";
                }
            }
        }

        private static string RemoveReservedUnsafeCharacters(string text)
        {
            return ReservedAndUnsafeCharacters.Aggregate(text, (current, character) => current.Replace(character, string.Empty));
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

        public static Collection GetCollection(int id, out string error)
        {
            error = "";
            var priceRequestEngine = new TcgPlayerRequestEngine();
            var cardRequestEngine = new ScryfallRequestEngine();

            using (var db = new FortyLifeDbContext())
            {
                var collection = db.Collections.Include(i => i.Cards).FirstOrDefault(i => i.CollectionId == id);

                if (collection != null)
                {
                    // We need to update pertinent information about the collection
                    collection.TcgMidValue = 0;

                    foreach (var card in collection.Cards)
                    {
                        var setName = cardRequestEngine.GetCard(card.Name, card.SetCode).SetName;
                        Price price;

                        if (card.Foil)
                        {
                            price = priceRequestEngine.CardPriceRequest(card.Name, setName)?
                                .First(i => i.SubTypeName == "Foil");
                        }
                        else
                        {
                            price = priceRequestEngine.CardPriceRequest(card.Name, setName)?
                                .First(i => i.SubTypeName == "Normal");
                        }

                        if (price?.MidPrice != null)
                        {
                            collection.TcgMidValue += price.MidPrice.Value;
                        }

                        return collection;
                    }
                }
            }

            error = "This collection does not exist or is private.";
            return new Collection();
        }

        public static ApplicationUser GetApplicationUser(string email)
        {
            if (ApplicationUserCache.Contains(email))
            {
                return (ApplicationUser)ApplicationUserCache[email];
            }

            using (var db = new FortyLifeDbContext())
            {
                var users = db.ApplicationUsers.Include(i => i.Collections.Select(j => j.Cards));
                var user = users.FirstOrDefault(i => i.Email == email);

                if (user != null)
                {
                    ApplicationUserCache.Set(email, user, DateTimeOffset.Now.AddDays(7));
                    return user;
                }
            }

            return null;
        }

        public static ApplicationUser GetApplicationUser(int id)
        {
            using (var db = new FortyLifeDbContext())
            {
                var users = db.ApplicationUsers.Include(i => i.Collections.Select(j => j.Cards));
                var user = users.FirstOrDefault(i => i.Id == id);

                if (user != null)
                {
                    if (ApplicationUserCache.Contains(user.Email))
                    {
                        return (ApplicationUser)ApplicationUserCache[user.Email];
                    }

                    ApplicationUserCache.Set(user.Email, user, DateTimeOffset.Now.AddDays(7));
                    return user;
                }

                return null;
            }
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
