using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using FortyLife.Data.Scryfall;
using Newtonsoft.Json;

namespace FortyLife.Data
{
    public class ScryfallRequestEngine : RequestEngine
    {
        private const string BaseSearchUri = "https://api.scryfall.com/cards/search";

        public ScryfallList Request(string requestUri)
        {
            // https://scryfall.com/docs/api#type-error (see: "Rate Limits and Good Citizenship")
            // try to delay the request time by 200 ms, so that in perfect sequence we can only hope to pull off 5 requests per second
            // scryfall will ban this IP if their endpoints are abused and they would like us to limit our requests to 10 per second, anyway
            Thread.Sleep(200); // TODO: better way to rate limit without shutting the thread down entirely
            // TODO: handle the 429 status code (if we ever even get it back) from scryfall

            var jsonResult = Get(BaseSearchUri + requestUri).Replace("_", string.Empty);
            return !string.IsNullOrEmpty(jsonResult) ? JsonConvert.DeserializeObject<ScryfallList>(jsonResult) : new ScryfallList();
        }

        public ScryfallList CardSearchRequest(string cardName)
        {
            return Request($"?q=name={cardName}");
        }

        public Card FirstCardFromSearch(string cardName)
        {
            var searchResultList = CardSearchRequest(cardName);
            //TODO: Make sure we're returning a valid card or else return null
            return searchResultList.Data.FirstOrDefault();
        }
    }
}
