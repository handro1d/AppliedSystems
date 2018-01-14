using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NIPF.Web.Models;

namespace AppliedSystems.Web.Controllers
{
    [RoutePrefix("Account")]
    public sealed class AccountController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<User, int> _signInManager;
        private readonly UserManager<User, int> _userManager;

        public AccountController(
            IAuthenticationManager authenticationManager,
            SignInManager<User, int> signInManager,
            UserManager<User, int> userManager)
        {
            if (authenticationManager == null)
            {
                throw new ArgumentNullException("authenticationManager");
            }

            if (signInManager == null)
            {
                throw new ArgumentNullException("signInManager");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userManager = userManager;

            // Update User manager to allow email addresses as usernames
            _userManager.UserValidator = new UserValidator<User, int>(_userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        [Route("~/Login")]
        [HttpGet, AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            if (_authenticationManager.User.Identity.IsAuthenticated)
            {
                // User has already logged in - redirect to home page
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        [Route("~/Login")]
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                // Invalid data - return view
                return View("Login", model);
            }

            // Try to sign in user with provided details
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            return result == SignInStatus.Success
                ? new HttpStatusCodeResult(HttpStatusCode.OK)
                : new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        [Route("Register")]
        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            if (_authenticationManager.User.Identity.IsAuthenticated)
            {
                // User is already logged in - redirect to home page
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new RegisterViewModel();

            return View("Register", viewModel);
        }

        [Route("Register")]
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var password = model.Password?.Trim();
            var passwordConfirmation = model.PasswordConfirmation?.Trim();

            if (!string.IsNullOrEmpty(password) 
                && !string.IsNullOrEmpty(passwordConfirmation) 
                && !password.Equals(passwordConfirmation))
            {
                // Passwords do not match, add relevant error message
                ModelState.AddModelError("passwordConfirmation", "Passwords do not match");
            }

            if (!ModelState.IsValid)
            {
                // Bad request, return to view & display property validation
                return View("Register", model);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var userCreationResult = await _userManager.CreateAsync(user, model.Password);

            if (!userCreationResult.Succeeded)
            {
                // User could not be created, return to view & display validtion
                ModelState.AddModelError("userCreation", "Could not create User");
                return View("Register", model);
            }

            // Log user in
            await _signInManager.SignInAsync(user, false, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            // Sign user out
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}