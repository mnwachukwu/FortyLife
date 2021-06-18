using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.Core;
using FortyLife.DataAccess;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        [Route("Spoilers")]
        public ActionResult Spoilers(string setCode, int? sortId)
        {
            var scryfallRequestEngine = new ScryfallRequestEngine();
            if (string.IsNullOrEmpty(setCode))
            {
                setCode = scryfallRequestEngine.LatestSet().Code;
            }
            List<Card> cards;

            if (sortId != null)
            {
                // TODO: We don't really need a new request every time they change sort order...
                switch (sortId)
                {
                    // default sort order, by collector number
                    default:
                        cards = scryfallRequestEngine.CardSetListRequest(setCode).Data
                            .OrderBy(i => Convert.ToInt32(i.CollectorNumber.GetNumber()))
                            .ToList();
                        break;

                    // sort by rarity then by collector number
                    case 1:
                        cards = scryfallRequestEngine.CardSetListRequest(setCode).Data
                            .OrderBy(i => Array.IndexOf(SpoilersViewModel.RarityOrder, i.Rarity))
                            .ThenBy(i => Convert.ToInt32(i.CollectorNumber.GetNumber()))
                            .ToList();
                        break;

                    // sort by name; no need to sort by collector number in this case
                    case 2:
                        cards = scryfallRequestEngine.CardSetListRequest(setCode).Data
                            .OrderBy(i => i.Name.Replace("\"", string.Empty))
                            .ToList();
                        break;

                    // sort by cmc then by collector number
                    case 3:
                        cards = scryfallRequestEngine.CardSetListRequest(setCode).Data
                            .OrderBy(i => i.Cmc)
                            .ThenBy(i => Convert.ToInt32(i.CollectorNumber.GetNumber()))
                            .ToList();
                        break;
                }
            }
            else
            {
                cards = scryfallRequestEngine.CardSetListRequest(setCode).Data
                    .OrderBy(i => Convert.ToInt32(i.CollectorNumber.GetNumber()))
                    .ToList();
            }

            var model = new SpoilersViewModel
            {
                Set = scryfallRequestEngine.SetRequest(setCode),
                Sets = scryfallRequestEngine.SetsRequest(),
                Cards = cards
            };

            return View(model);
        }
    }
}