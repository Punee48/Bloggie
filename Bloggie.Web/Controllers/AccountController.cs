using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class AccountController : Controller
    {
        //Constructor
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        // GET: AccountController

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.email
            };


            var identityUserResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityUserResult.Succeeded)
            {
                //Assigning User Role to the User
                var roleIdentityUser = await _userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityUser.Succeeded)
                {
                    //Show Success Message
                    return RedirectToAction("Register");
                }
            }

            //show error message
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var signInUser = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (signInUser != null && signInUser.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            System.Console.WriteLine("Logout is Successful");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
