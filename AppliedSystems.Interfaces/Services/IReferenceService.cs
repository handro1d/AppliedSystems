using System.Collections.Generic;
using AppliedSystems.Domain;

namespace AppliedSystems.Interfaces
{
    public interface IReferenceService
    {
        IEnumerable<TReferenceEntity> Get<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity;

        TReferenceEntity Get<TReferenceEntity>(int id) where TReferenceEntity : class, IReferenceEntity, new();

        TReferenceEntity GetByCode<TReferenceEntity>(string entityCode) where TReferenceEntity : class, IReferenceEntity, new();
    }
}
