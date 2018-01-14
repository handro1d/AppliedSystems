using System.Diagnostics.CodeAnalysis;
using AppliedSystems.Interfaces;
using AppliedSystems.Services;
using Unity;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    public static class ServiceInstaller
    {
        public static void InstallServices(this IUnityContainer container)
        {
            container.RegisterType<IAppSettings, AppSettings>();
            container.RegisterType<IReferenceService, ReferenceService>();
            container.RegisterType<IPolicyService, PolicyService>();
        }
    }
}