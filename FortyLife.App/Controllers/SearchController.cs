﻿using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.DataAccess;

namespace FortyLife.App.Controllers
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
                return RedirectToAction("CardDetails", "Search", new { cardName = results.Data[0].Name, setCode = results.Data[0].Set });
            }

            return View("Results",
                new SearchResultsViewModel
                {
                    NameSearch = cardName,
                    Results = results
                });
        }

        [Route("CardDetails")]
        public ActionResult CardDetails(string cardName, string setCode)
        {
            var requestEngine = new ScryfallRequestEngine();
            var card = requestEngine.GetCard(cardName, setCode);
            return card != null ? View("CardDetails", card) : View("~/Views/Shared/CardNotFound.cshtml", null, cardName);
        }
    }
}