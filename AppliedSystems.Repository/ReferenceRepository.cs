using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AppliedSystems.Domain;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Repository
{
    [ExcludeFromCodeCoverage]
    public sealed class ReferenceRepository : IReferenceRepository
    {
        private readonly AppliedSystemsContext _context;

        public ReferenceRepository(AppliedSystemsContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public IEnumerable<TReferenceEntity> GetReferenceEntities<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity
        {
            return _context.Set<TReferenceEntity>();
        }

        public TReferenceEntity GetReferenceEntity<TReferenceEntity>(int id) where TReferenceEntity : class, IReferenceEntity
        {
            return _context.Set<TReferenceEntity>().Find(id);
        }

        public TReferenceEntity GetReferenceEntityByCode<TReferenceEntity>(string referenceCode) where TReferenceEntity : class, IReferenceEntity
        {
          return _context.Set<TReferenceEntity>().FirstOrDefault(r => r.Code.Equals(referenceCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
