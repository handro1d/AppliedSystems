using System.Collections.Generic;
using AppliedSystems.Domain;

namespace AppliedSystems.Interfaces
{
    public interface IReferenceRepository
    {
        IEnumerable<TReferenceEntity> GetReferenceEntities<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity;

        TReferenceEntity GetReferenceEntity<TReferenceEntity>(int id) where TReferenceEntity : class, IReferenceEntity;

        TReferenceEntity GetReferenceEntityByCode<TReferenceEntity>(string referenceCode) where TReferenceEntity : class, IReferenceEntity;
    }
}
