using System.Web.Mvc;
using FortyLife.App.Models;

namespace FortyLife.App.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [RequireRequestValue("model")]
        public ActionResult Login(LoginViewModel model)
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [RequireRequestValue("model")]
        public ActionResult Register(RegisterViewModel model) // TODO: Account creation
        {
            return View();
        }
    }
}