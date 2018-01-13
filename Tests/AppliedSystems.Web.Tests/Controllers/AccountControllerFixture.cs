using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Tests.Common;
using AppliedSystems.Web.Controllers;
using AppliedSystems.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Moq;
using NIPF.Web.Models;
using NUnit.Framework;

namespace AppliedSystems.Web.Tests
{
    [TestFixture]
    internal sealed class AccountControllerFixture
    {
        private AccountControllerBuilder _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new AccountControllerBuilder();
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfAuthenticationManagerIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _controller.WithAuthenticationManager(null).Build();
            }, "authenticationManager");
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfSignInManagerIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _controller.WithSignInManager(null).Build();
            }, "signInManager");
        }

        #region Login

        [Test]
        public void Login_ShouldRedirectAuthenticatedUserToHomePage()
        {
            // Setup Authentication Manager
            var authenticatedIdentity = new MockIdentity(true);
            var authenticatedUser = new ClaimsPrincipal(authenticatedIdentity);

            var authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build();

            // Call login method
            ActionResult result = _controller
                .WithAuthenticationManager(authenticationManager.Object).Build()
                .Login();

            // Verify user is redirected to Home page
            result.AssertIsRedirect("Home", "Index");
        }

        [Test]
        public void Login_ShouldDisplayLoginPageForUnauthenticatedUser()
        {
            // Setup Authentication Manager
            var unAuthenticatedIdentity = new MockIdentity();
            var authenticatedUser = new ClaimsPrincipal(unAuthenticatedIdentity);

            var authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build();

            // Call login method
            ActionResult result = _controller
                .WithAuthenticationManager(authenticationManager.Object).Build()
                .Login();

            // Verify Login view is returned
            result.AssertIsView("Login");
        }

        [Test]
        public void Login_ShouldSetReturnUrlIfProvided()
        {
            // Setup Authentication Manager
            var unAuthenticatedIdentity = new MockIdentity();
            var authenticatedUser = new ClaimsPrincipal(unAuthenticatedIdentity);

            var authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build();

            // Call login method
            ActionResult result = _controller
                .WithAuthenticationManager(authenticationManager.Object).Build()
                .Login("/TestReturnUrl");

            // Verify expected result
            ViewResult returnedView = result.AssertIsView("Login");
            Assert.AreEqual("/TestReturnUrl", returnedView.ViewBag.ReturnUrl.ToString());
        }

        [Test]
        public void Login_ShouldReturnViewIfModelIsInvalid()
        {
            // Setup invalid model
            AccountController controller = _controller.Build();
            controller.ModelState.AddModelError("Test Error", "Test Error Message");

            // Call Login method
            ActionResult result = controller.Login(new LoginViewModel()).Result;

            // Verify result
            result.AssertIsView("Login");
        }

        [Test]
        public void Login_ShouldAddModelErrorOnSignInFailure()
        {
            // Setup sign in manager
            var userManager = new MockUserManager();
            var authenticationManager = new FakeAuthenticationManager().Build();
            var signInManager = new MockSignInManager(userManager, authenticationManager.Object);

            AccountController controller = _controller
                .WithSignInManager(signInManager).Build();

            // Call Login method
            var request = new LoginViewModel {Email = "test@test.com", Password = "Test"};
            ActionResult result = controller.Login(request).Result;

            // Verify result
            result.AssertIsView("Login");
            Assert.IsFalse(controller.ModelState.IsValid);

            var erroroMessages = controller.ModelState[string.Empty].Errors.SelectMany(
                modelError => new[] {modelError.ErrorMessage.ToString()}).ToList();

            erroroMessages.AssertCollectionContainsItems("Username or password was incorrect");
        }

        [Test]
        public void Login_ShouldLoginUserWithCorrectCredentials()
        {
            // Setup dependencies
            var signInManager = new MockSignInManager()
                .WithPasswordSignInStatus(SignInStatus.Success);

            // Call Login method
            var request = new LoginViewModel {Email = "test@test.com", Password = "testHash"};
            ActionResult result = _controller
                .WithSignInManager(signInManager).Build()
                .Login(request).Result;

            // Verify result
            result.AssertIsRedirect("Home", "Index");
        }

        #endregion

        #region Register

        [Test]
        public void Register_ShouldRedirectAuthenticatedUserToHomePage()
        {
            // Setup Authentication Manager
            var authenticatedIdentity = new MockIdentity(true);
            var authenticatedUser = new ClaimsPrincipal(authenticatedIdentity);

            var authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build();

            // Call login method
            ActionResult result = _controller
                .WithAuthenticationManager(authenticationManager.Object).Build()
                .Register();

            // Verify user is redirected to Home page
            result.AssertIsRedirect("Home", "Index");
        }

        [Test]
        public void Register_ShouldDisplayRegisterPageForUnAuthenticatedUser()
        {
            // Setup Authentication Manager
            var unAuthenticatedIdentity = new MockIdentity();
            var authenticatedUser = new ClaimsPrincipal(unAuthenticatedIdentity);

            var authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build();

            // Call login method
            var result = _controller
                .WithAuthenticationManager(authenticationManager.Object).Build()
                .Register();

            // Verify Login view is returned
            result.AssertIsView("Register");
        }

        [Test]
        public void Register_ShouldReturnViewIfModelIsInvalid()
        {
            // Setup invalid model
            var controller = _controller.Build();
            controller.ModelState.AddModelError("Test Error", "Test Error Message");

            // Call register
            var result = controller.Register(new RegisterViewModel()).Result;

            // Verify result
            result.AssertIsView("Register");
        }

        [Test]
        public void Register_ShouldReturnViewIfPasswordIsNotConfirmed()
        {
            // Setup request
            var request = new RegisterViewModel {Password = "Password", PasswordConfirmation = "IDontMatch"};

            // Call register
            var result = _controller.Build().Register(request).Result;

            // Verify result
            result.AssertIsView("Register");
        }

        [Test]
        public void Register_ShouldCreateUser()
        {
            // Setup dependencies
            var request = ConstructDefaultRegisterViewModel();

            var userStore = new FakeUserStore()
                .WithAbilityToCreateUser().Build();

            var passwordHasher = new FakePasswordHasher().Build();
            var userManager = new MockUserManager(userStore.Object, passwordHasher.Object);

            // Call register
            var result = _controller
                .WithUserManager(userManager).Build()
                .Register(request).Result;

            // Verify result
            userStore.Verify(
                u => u.CreateAsync(It.Is<User>(user =>
                    user.Email == request.Email
                    && user.UserName == request.Email)), Times.Once);
        }

        [Test]
        public void Register_ShouldReturnViewIfUserCannotBeCreated()
        {
            // Setup dependencies
            var request = ConstructDefaultRegisterViewModel();
            var userManager = new MockUserManager()
                .WithUserCreationResult(IdentityResult.Failed());

            // Call register
            var result = _controller
                .WithUserManager(userManager).Build()
                .Register(request).Result;

            // Verify result
            result.AssertIsView("Register");
        }

        [Test]
        public void Register_ShouldSignInUserIfCreated()
        {
            // Setup dependencies
            var request = ConstructDefaultRegisterViewModel();

            var userManager = new MockUserManager()
                .WithUserCreationResult(IdentityResult.Success);

            var authenticationManager = new FakeAuthenticationManager().Build();
            var signInManager = new MockSignInManager(userManager, authenticationManager.Object);

            // Call register
            var result = _controller
                .WithUserManager(userManager)
                .WithSignInManager(signInManager).Build()
                .Register(request).Result;

            // Verify result
            authenticationManager.Verify(
                a => a.SignIn(It.IsAny<AuthenticationProperties>(), It.IsAny<ClaimsIdentity[]>()), Times.Once);

            result.AssertIsRedirect("Home", "Index");
        }

        private static RegisterViewModel ConstructDefaultRegisterViewModel()
        {
            return new RegisterViewModel
            {
                Email = "test@test.com",
                FirstName = "Test",
                Surname = "User",
                Password = "Passw0rd!",
                PasswordConfirmation = "Passw0rd!"
            };
        }

        #endregion
    }
}
