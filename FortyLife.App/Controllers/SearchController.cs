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
            cardName = cardName.Trim();

            if (cardName.Length <= 2) return RedirectToAction("Index", "Home");

            var requestEngine = new ScryfallRequestEngine();
            var results = requestEngine.CardSearchRequest(cardName);

            if (results.TotalCards == 1)
            {
                return RedirectToAction("CardDetails", "Search", new { cardName = results.Data[0].Name });
            }

            return View("Results",
                new SearchResultsViewMovel
                {
                    NameSearch = cardName,
                    Results = results
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