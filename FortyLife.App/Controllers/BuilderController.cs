using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.Core;
using FortyLife.DataAccess;
using FortyLife.DataAccess.TCGPlayer;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.App.Controllers
{
    public class BuilderController : Controller
    {
        public ActionResult ViewCollection(int id)
        {
            var collection = ApplicationUserEngine.GetCollection(id, out var error);

            if (User.Identity.IsAuthenticated)
            {
                var email = ((ClaimsIdentity) User.Identity).Claims.First(i =>
                    i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                var user = ApplicationUserEngine.GetApplicationUser(email);

                ViewBag.IsOwner = user.Collections.Any(i => i.CollectionId == collection.CollectionId);
            }
            else
            {
                ViewBag.IsOwner = false;
            }

            // TODO: Up the view count and add it to an audit log for tracking so the system can't be cheated

            if (!string.IsNullOrEmpty(error))
            {
                TempData["AlertMsg"] = "<br /><div class=\"alert alert-danger alert-dismissible\">" +
                                       "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                       $"<strong>{error}</div>";

                return View("Error");
            }

            return View("Collection", collection);
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
        public ActionResult EditCollection(int id)
        {
            var email = ((ClaimsIdentity)User.Identity).Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var user = ApplicationUserEngine.GetApplicationUser(email);

            if (user.Collections.Any(i => i.CollectionId == id))
            {
                var collection = user.Collections.First(i => i.CollectionId == id);
                var model = new CollectionModel
                {
                    Collection = collection,
                    RawList = CardListParsingEngine.GetCardList(collection.Cards)
                };

                return View("EditCollection", model);
            }

            TempData["AlertMsg"] = "<br /><div class=\"alert alert-danger alert-dismissible\">" +
                                   "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                   "<strong>You are not authorized to edit this collection.</div>";

            return View("Error");
        }

        [Authorize]
        public ActionResult SaveCollection(CollectionModel model)
        {
            var email = ((ClaimsIdentity)User.Identity).Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var collectionCards = CardListParsingEngine.ParseCardList(model.RawList, out var error);

            if (string.IsNullOrEmpty(model.Collection.Name))
            {
                model.Collection.Name = "Unnamed Collection";
            }
            else
            {
                model.Collection.Name = model.Collection.Name.Trim();
            }

            if (!string.IsNullOrEmpty(error))
            {
                TempData["AlertMsg"] = "<br /><div class=\"alert alert-danger alert-dismissible\">" +
                                       "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                       $"<strong>{error}</div>";

                return View("EditCollection", model);
            }

            if (!CardListParsingEngine.VerifyCardList(collectionCards, out collectionCards, out error))
            {
                TempData["AlertMsg"] = "<br /><div class=\"alert alert-danger alert-dismissible\">" +
                                       "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                       $"<strong>{error}</div>";

                return View("EditCollection", model);
            }

            model.Collection.Cards = collectionCards;

            // only change the date if it's not a new collection in order to keep the dates the same for the first save
            if (model.Collection.CollectionId > 0)
            {
                model.Collection.LastEditDate = DateTime.Now;
            }

            ApplicationUserEngine.DeleteAllCardsInCollection(model.Collection);
            ApplicationUserEngine.AddOrUpdateUserCollection(email, model.Collection, out error);

            if (!string.IsNullOrEmpty(error))
            {
                TempData["AlertMsg"] = "<br /><div class=\"alert alert-danger alert-dismissible\">" +
                                       "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                       $"<strong>{error}</div>";

                return View("EditCollection", model);
            }

            ApplicationUserEngine.UpdateUserInCache(email);

            TempData["AlertMsg"] = "<br /><div class=\"alert alert-success alert-dismissible\">" +
                                   "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                   "<strong>The Collection was saved successfully.</div>";

            return RedirectToAction("EditCollection", new { id = model.Collection.CollectionId });
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
        public ActionResult EditDeck(int id)
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