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
        public ActionResult SaveCollection(Collection model)
        {
            return View();
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