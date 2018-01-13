using System.Diagnostics.CodeAnalysis;
using Unity;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    public static class ServiceInstaller
    {
        public static void InstallServices(this IUnityContainer container)
        {
            //container.RegisterType<IAppSettings, AppSettings>();
            //container.RegisterType<IMembershipService, MembershipService>();
            //container.RegisterType<IReferenceService, ReferenceService>();
            //container.RegisterType<ICompetitionService, CompetitionService>();
        }
    }
}