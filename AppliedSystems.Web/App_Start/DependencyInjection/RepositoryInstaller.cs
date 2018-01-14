using System.Diagnostics.CodeAnalysis;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using AppliedSystems.Repository;
using AppliedSystems.Services;
using Unity;
using Unity.Lifetime;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryInstaller
    {
        public static void InstallRepositories(this IUnityContainer container)
        {
            container.RegisterType<ISqlExceptionRetryPolicy, SqlExceptionRetryPolicy>();
            container.RegisterType<IReferenceRepository, ReferenceRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IAppliedSystemsRepository<InsurancePolicy>, PolicyRepository>(new PerResolveLifetimeManager());
        }
    }
}