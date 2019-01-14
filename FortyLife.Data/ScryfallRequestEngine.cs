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
            var jsonResult = Get(BaseSearchUri + requestUri);
            return JsonConvert.DeserializeObject<ScryfallList>(jsonResult);
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
