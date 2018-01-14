using System.Data.Entity;
using System.Linq;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Repository
{
    public sealed class PolicyRepository : AppliedSystemsRepository<InsurancePolicy>
    {
        public PolicyRepository(
            AppliedSystemsContext context, 
            IAppSettings appSettings,
            ISqlExceptionRetryPolicy sqlExceptionRetryPolicy) 
            : base(context, appSettings, sqlExceptionRetryPolicy)
        {
        }

        public override InsurancePolicy Add(InsurancePolicy t)
        {
            var addedEntity = base.Add(t);

            return Context.Policies
                .Where(p => p.Id == addedEntity.Id)
                .Include("Drivers.Occupation")
                .FirstOrDefault();
        }
    }
}
