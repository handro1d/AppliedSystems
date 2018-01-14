using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Domain;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Services.Tests
{
    internal sealed class MockReferenceRepository : IReferenceRepository
    {
        private readonly List<IReferenceEntity> _entities;

        public MockReferenceRepository()
        {
            _entities = new List<IReferenceEntity>();
        }

        public MockReferenceRepository With<TReferenceEntity>(TReferenceEntity entity)
            where TReferenceEntity : IReferenceEntity
        {
            _entities.Add(entity);
            return this;
        }

        public IEnumerable<TReferenceEntity> GetReferenceEntities<TReferenceEntity>() 
            where TReferenceEntity : class, IReferenceEntity
        {
            return _entities.Where(e => e.GetType() == typeof(TReferenceEntity)).Select(e => (TReferenceEntity) e);
        }

        public TReferenceEntity GetReferenceEntity<TReferenceEntity>(int id) where TReferenceEntity : class, IReferenceEntity
        {
            return GetReferenceEntities<TReferenceEntity>().Where(e => e.Id == id) as TReferenceEntity;
        }

        public TReferenceEntity GetReferenceEntityByCode<TReferenceEntity>(string referenceCode) 
            where TReferenceEntity : class, IReferenceEntity
        {
            return GetReferenceEntities<TReferenceEntity>().FirstOrDefault(e => e.Code.Equals(referenceCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
