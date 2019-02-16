using System.Web.Mvc;
using FortyLife.App.Models;

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
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel model) // TODO: Account creation
        {
            return View();
        }
    }
}