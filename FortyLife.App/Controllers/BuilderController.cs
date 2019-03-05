using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.App.Controllers
{
    public class BuilderController : Controller
    {
        public ActionResult ViewCollection(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult NewCollection()
        {
            var model = new CollectionModel
            {
                Collection = new Collection
                {
                    Cards = new List<CollectionCard>(),
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now
                }
            };

            return View("EditCollection", model);
        }

        [Authorize]
        public ActionResult EditCollection()
        {
            return View();
        }

        [Authorize]
        public ActionResult SaveCollection(CollectionModel model)
        {
            var collection = Core.CardListParsingEngine.ParseCardList(model.RawList, out var error);

            if (!string.IsNullOrEmpty(error))
            {
                // TODO: use a temp message here to alert them of the failed action
                return View(model);
            }

            // TODO: after doing the stuff, return this page anyway, but with a temp message with success!
            return View(model);
        }

        public ActionResult ViewDeck(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult NewDeck()
        {
            return View();
        }

        [Authorize]
        public ActionResult EditDeck()
        {
            return View();
        }

        [Authorize]
        public ActionResult SaveDeck()
        {
            return View();
        }
    }
}