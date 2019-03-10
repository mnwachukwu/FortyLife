using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using FortyLife.DataAccess.Scryfall;
using Newtonsoft.Json;

namespace FortyLife.DataAccess
{
    public class ScryfallRequestEngine : RequestEngine
    {
        private const string BaseSearchUri = "https://api.scryfall.com/cards/search";
        private const string SetSearchUri = "https://scryfall.com/set/";

        private T Request<T>(string requestUri) where T : new()
        {
            // https://scryfall.com/docs/api#type-error (see: "Rate Limits and Good Citizenship")
            // try to delay the request time by 200 ms, so that in perfect sequence we can only hope to pull off 5 requests per second
            // scryfall will ban this IP if their endpoints are abused and they would like us to limit our requests to 10 per second, anyway
            Thread.Sleep(200); // TODO: better way to rate limit without shutting the thread down entirely
            // TODO: handle the 429 status code (if we ever even get it back) from scryfall

            var jsonResult = Get(requestUri).Replace("_", string.Empty);

            return !string.IsNullOrEmpty(jsonResult) ? JsonConvert.DeserializeObject<T>(jsonResult) : new T();
        }

        public ScryfallList<Card> CardSearchRequest(string cardName)
        {
            var request = Request<ScryfallList<Card>>($"{BaseSearchUri}?q=name={cardName}");

            if (request.Data != null)
            {
                for (var i = 0; i < request.Data.Count; i++)
                {
                    if (request.Data[i].Digital)
                    {
                        request.Data[i] = FirstCardFromSearch(request.Data[i].Name);
                    }
                }
            }

            return request;
        }

        public Card FirstCardFromSearch(string cardName, string setCode = "")
        {
            using (var db = new FortyLifeDbContext())
            {
                Card card;

                if (!string.IsNullOrEmpty(setCode))
                {
                    if (db.Cards.Any(i => i.Name == cardName && i.Set == setCode && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 7))
                    {
                        var cards = db.Cards.Include(i => i.CardFaces);

                        card = cards.FirstOrDefault(i => i.Name == cardName && i.Set == setCode);

                        if (card != null)
                        {
                            // prep list data by parsing string data from db
                            foreach (var i in card.CardFaces)
                            {
                                if (i.ColorsString != null)
                                {
                                    i.Colors = i.ColorsString.Split(',').ToList();
                                }
                            }

                            card.Colors = card.ColorsString?.Split(',').ToList();
                            card.ColorIdentity = card.ColorIdentityString?.Split(',').ToList();
                            card.Games = card.GamesString?.Split(',').ToList();
                            card.MultiverseIds = card.MultiverseIdsString?.Split(',').ToList();

                            // return prepped card
                            return card;
                        }
                    }
                }
                else
                {
                    if (db.Cards.Any(i => i.Name == cardName && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 7))
                    {
                        var cards = db.Cards.Include(i => i.CardFaces);

                        return cards.FirstOrDefault(i => i.Name == cardName);
                    }
                }

                var searchResultList = CardPrintingsRequest(cardName);

                if (searchResultList != null)
                {
                    card = !string.IsNullOrEmpty(setCode)
                        ? searchResultList.Data?.FirstOrDefault(i =>
                            i.Name == cardName &&
                            string.Equals(i.Set, setCode, StringComparison.CurrentCultureIgnoreCase))
                        : searchResultList.Data?.FirstOrDefault(i => i.Name == cardName);

                    if (card != null)
                    {
                        card.CacheDate = DateTime.Now;

                        if (card.ImageUris == null)
                            card.ImageUris = new ImageUris();

                        if (card.IsDoubleFaced)
                        {
                            card.CardFaces[0].CacheDate = DateTime.Now;
                            card.CardFaces[1].CacheDate = DateTime.Now;

                            if (card.CardFaces[0].ImageUris == null)
                                card.CardFaces[0].ImageUris = new ImageUris();

                            if (card.CardFaces[1].ImageUris == null)
                                card.CardFaces[1].ImageUris = new ImageUris();

                            foreach (var i in card.CardFaces)
                            {
                                if (i.Colors != null)
                                {
                                    i.ColorsString = PrimitiveListHandler.ToString(i.Colors);
                                }
                            }
                        }

                        if (card.Colors != null)
                        {
                            card.ColorsString = PrimitiveListHandler.ToString(card.Colors);
                        }

                        if (card.ColorIdentity != null)
                        {
                            card.ColorIdentityString = PrimitiveListHandler.ToString(card.ColorIdentity);
                        }

                        if (card.Games != null)
                        {
                            card.GamesString = PrimitiveListHandler.ToString(card.Games);
                        }

                        if (card.MultiverseIds != null)
                        {
                            card.MultiverseIdsString = PrimitiveListHandler.ToString(card.MultiverseIds);
                        }

                        db.Cards.AddOrUpdate(card);

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException dbEx)
                        {
                            foreach (var validationErrors in dbEx.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    Trace.TraceInformation("Property: {0} Error: {1}",
                                        validationError.PropertyName,
                                        validationError.ErrorMessage);
                                }
                            }

                            throw;
                        }
                    }

                    return card;
                }
            }

            return null;
        }

        public ScryfallList<Card> CardPrintingsRequest(string cardName)
        {
            var results = Request<ScryfallList<Card>>($"{BaseSearchUri}?q=name={cardName}&unique=prints");
            if (results.TotalCards > 0)
            {
                results.Data = results.Data.Where(i => i.Digital == false && i.Name == cardName).ToList();

                return results;
            }

            return null;
        }

        public List<SetName> CardPrintingsSetNames(string cardName)
        {
            var setNames = new List<SetName>();

            foreach (var printing in CardPrintingsRequest(cardName).Data)
            {
                setNames.Add(new SetName
                {
                    Code = printing.Set,
                    Name = printing.SetName,
                    Rarity = printing.Rarity
                });
            }

            return setNames;
        }

        public Set CardSetRequest(string setUri)
        {
            return Request<Set>(setUri);
        }

        public int SetCardCount(string setUri)
        {
            return CardSetRequest(setUri).CardCount;
        }

        public List<Ruling> RulingsRequest(string rulingsUri)
        {
            using (var db = new FortyLifeDbContext())
            {
                if (db.Rulings.Any(i => i.RulingsUri == rulingsUri && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 7))
                {
                    return db.Rulings.Where(i => i.RulingsUri == rulingsUri).ToList();
                }

                var rulings = Request<ScryfallList<Ruling>>(rulingsUri).Data;

                if (rulings != null)
                {
                    foreach (var ruling in rulings)
                    {
                        ruling.RulingsUri = rulingsUri;
                        ruling.CacheDate = DateTime.Now;
                        db.Rulings.AddOrUpdate(ruling);
                        db.SaveChanges();
                    }
                }

                return rulings;
            }
        }
    }
}
