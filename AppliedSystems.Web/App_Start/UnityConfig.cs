using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace AppliedSystems.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.InstallAuthentication();
            container.InstallRepositories();
            container.InstallServices();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}