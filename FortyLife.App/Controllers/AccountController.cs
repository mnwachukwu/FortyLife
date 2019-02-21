using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FortyLife.App.Models;
using FortyLife.DataAccess;
using FortyLife.DataAccess.UserAccount;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FortyLife.App.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!UserAuthenticator.IsValid(model.Email, model.Password))
            {
                // invalid username or password
                ModelState.AddModelError("", "Invalid email or password.");

                return View(model);
            }

            // load the user in memory
            var user = ApplicationUserEngine.GetApplicationUser(model.Email);

            // create our claim
            var ident = new ClaimsIdentity(
                new[]
                {
                    // adding following 2 claim just for supporting default antiforgery provider
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                    // Identity stuff to personalize the site
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name, $"{user.DisplayName}#{user.Id}"),

                    // add a role for a basic user
                    // TODO: we'll need to add more roles for users for administrative permissions
                    new Claim(ClaimTypes.Role, "User")
                },
                DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication
                .SignIn(new AuthenticationProperties { IsPersistent = false, ExpiresUtc = DateTimeOffset.Now.AddDays(1) }, ident);

            return RedirectToAction("Index", "Home"); // auth succeed, take em home
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!model.AgreeToGuidelines)
            {
                ModelState.AddModelError("", "You must agree to adhere to the community guidelines in order to register.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (ApplicationUserEngine.CreateAccount(model.Email, model.Password))
                {
                    return View("RegisterSuccess", model: model.Email);
                }

                ModelState.AddModelError("", "That email address is already associated with a Forty Life account.");
                return View(model);
            }

            ModelState.AddModelError("", "Registration has failed, please try again.");
            return View(model);
        }
    }
}