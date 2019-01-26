using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using FortyLife.Data.Scryfall;
using Newtonsoft.Json;
using static System.String;

namespace FortyLife.Data
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

            var jsonResult = Get(requestUri).Replace("_", Empty);

            if (typeof(T) == typeof(ScryfallList<Card>))
                jsonResult = jsonResult.Replace("1v1", "_1v1"); // variables don't start with numbers, so replace the json

            return !IsNullOrEmpty(jsonResult) ? JsonConvert.DeserializeObject<T>(jsonResult) : new T();
        }

        public ScryfallList<Card> CardSearchRequest(string cardName)
        {
            var request = Request<ScryfallList<Card>>($"{BaseSearchUri}?q=name={cardName}");

            for (var i = 0; i < request.Data.Count; i++)
            {
                if (request.Data[i].Digital)
                {
                    request.Data[i] = FirstCardFromSearch(request.Data[i].Name);
                }
            }

            return request;
        }

        public Card FirstCardFromSearch(string cardName, string setCode = "")
        {
            var searchResultList = CardPrintingsRequest(cardName);

            return !IsNullOrEmpty(setCode)
                ? searchResultList.Data?.FirstOrDefault(i =>
                    i.Name == cardName && string.Equals(i.Set, setCode, StringComparison.CurrentCultureIgnoreCase))
                : searchResultList.Data?.FirstOrDefault(i => i.Name == cardName);
        }

        public ScryfallList<Card> CardPrintingsRequest(string cardName)
        {
            var results = Request<ScryfallList<Card>>($"{BaseSearchUri}?q=name={cardName}&unique=prints");
            results.Data = results.Data.Where(i => i.Digital == false && i.Name == cardName).ToList();

            return results;
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

        public ScryfallList<Ruling> RulingsRequest(string rulingsUri)
        {
            return Request<ScryfallList<Ruling>>(rulingsUri);
        }
    }
}
