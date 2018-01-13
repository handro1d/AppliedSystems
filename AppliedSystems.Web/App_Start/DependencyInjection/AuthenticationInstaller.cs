using System.Diagnostics.CodeAnalysis;
using System.Web;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using AppliedSystems.Repository;
using AppliedSystems.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Unity;
using Unity.Injection;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    public static class AuthenticationInstaller
    {
        public static void InstallAuthentication(this IUnityContainer container)
        {
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o =>
                HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<UserManager<User, int>, AppliedSystemsUserManager>();
            container.RegisterType<IAppliedSystemsPasswordHasher, Services.PasswordHasher>();

            container.RegisterType<IUserStore<User, int>, UserRepository>();
        }
    }
}