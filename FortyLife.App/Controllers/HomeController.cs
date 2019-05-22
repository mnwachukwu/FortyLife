using System;
using System.Linq;
using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.DataAccess;

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
        
        public ActionResult Spoilers(string setCode)
        {
            var scryfallRequestEngine = new ScryfallRequestEngine();
            if (string.IsNullOrEmpty(setCode))
            {
                setCode = scryfallRequestEngine.LatestSet().Code;
            }
            var model = new SpoilersViewModel
            {
                Set = setCode,
                SetName = scryfallRequestEngine.SetRequest(setCode).Name,
                Sets = scryfallRequestEngine.SetsRequest(),
                Cards = scryfallRequestEngine.CardSetListRequest(setCode).Data.OrderBy(i => Convert.ToInt32(i.CollectorNumber)).ToList()
            };

            return View(model);
        }
    }
}