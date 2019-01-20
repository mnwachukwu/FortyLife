using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortyLife.Data;
using FortyLife.Models;

namespace FortyLife.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Results(string cardName)
        {
            var requestEngine = new ScryfallRequestEngine();

            return View("Results",
                new SearchResultsViewMovel
                {
                    NameSearch = cardName,
                    Results = requestEngine.CardSearchRequest(cardName)
                });
        }

        public ActionResult CardDetails(string cardName)
        {
            var requestEngine = new ScryfallRequestEngine();
            var card = requestEngine.FirstCardFromSearch(cardName);
            return card != null ? View("CardDetails", card) : View("~/Views/Shared/CardNotFound.cshtml", null, cardName);
        }
    }
}