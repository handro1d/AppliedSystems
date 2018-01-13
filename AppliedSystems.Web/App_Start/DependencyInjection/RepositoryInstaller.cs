using System.Diagnostics.CodeAnalysis;
using Unity;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryInstaller
    {
        public static void InstallRepositories(this IUnityContainer container)
        {
            //container.InstallDefaultEtainRetryPolicies();
            //container.RegisterType<INipfRepository<Member>, BaseNipfRepository<Member>>(new PerResolveLifetimeManager());
            //container.RegisterType<ICompetitionRepository, CompetitionRepository>(new PerResolveLifetimeManager());
            //container.RegisterType<IReferenceRepository, ReferenceRepository>(new PerResolveLifetimeManager());
        }
    }
}