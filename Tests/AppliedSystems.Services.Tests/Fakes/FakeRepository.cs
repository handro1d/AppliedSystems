using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Domain;
using AppliedSystems.Interfaces;
using Moq;

namespace AppliedSystems.Services.Tests
{
    internal sealed class FakeRepository<TEntity> where TEntity : IEntity
    {
        private readonly Mock<IAppliedSystemsRepository<TEntity>> _repository;
        private readonly List<TEntity> _entities;

        public FakeRepository()
        {
            _repository = new Mock<IAppliedSystemsRepository<TEntity>>();
            _entities = new List<TEntity>();
        }

        public FakeRepository<TEntity> With(TEntity entity)
        {
            _entities.Add(entity);
            return this;
        }

        public Mock<IAppliedSystemsRepository<TEntity>> Build()
        {
            _repository
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns((int id) => _entities.FirstOrDefault(e => e.Id == id));

            return _repository;
        }
    }
}
