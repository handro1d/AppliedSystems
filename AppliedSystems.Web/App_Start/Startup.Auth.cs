using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AppliedSystems.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "AppliedSystemsTC",
                CookieHttpOnly = false,
                ExpireTimeSpan = TimeSpan.FromHours(6),
                SlidingExpiration = true,
                LoginPath = new PathString("/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                Provider = new CookieAuthenticationProvider(),
                ReturnUrlParameter = "returnUrl"
            });
        }
    }
}