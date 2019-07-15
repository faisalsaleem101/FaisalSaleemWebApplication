using FaisalLearningProjectMVC.Models.AccountModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> UserMgr;
        private readonly SignInManager<AppUser> SignInMgr;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await SignInMgr.PasswordSignInAsync("TestUser", model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Your account or password is incorrect.";
            }

            return View(model);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}