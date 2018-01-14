using System;
using System.Collections.Generic;
using AppliedSystems.Common.Exceptions;
using AppliedSystems.Domain;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Services
{
    public sealed class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceService(IReferenceRepository referenceRepository)
        {
            if (referenceRepository == null)
            {
                throw new ArgumentNullException("referenceRepository");
            }

            _referenceRepository = referenceRepository;
        }

        public IEnumerable<TReferenceEntity> Get<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity
        {
            return _referenceRepository.GetReferenceEntities<TReferenceEntity>();
        }

        public TReferenceEntity Get<TReferenceEntity>(int id) where TReferenceEntity : class, IReferenceEntity, new()
        {
            var returnedReferenceEntity = _referenceRepository.GetReferenceEntity<TReferenceEntity>(id);

            if (returnedReferenceEntity == null)
            {
                throw new EntityNotFoundException<TReferenceEntity>(new TReferenceEntity());
            }

            return returnedReferenceEntity;
        }

        public TReferenceEntity GetByCode<TReferenceEntity>(string entityCode) where TReferenceEntity : class, IReferenceEntity, new()
        {
            var returnedReferenceEntity = _referenceRepository.GetReferenceEntityByCode<TReferenceEntity>(entityCode);

            if (returnedReferenceEntity == null)
            {
                throw new EntityNotFoundException<TReferenceEntity>(new TReferenceEntity {Code = entityCode});
            }

            return returnedReferenceEntity;
        }
    }
}
