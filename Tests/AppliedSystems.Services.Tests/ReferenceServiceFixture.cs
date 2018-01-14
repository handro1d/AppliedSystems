using System.Linq;
using AppliedSystems.Common.Exceptions;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Tests.Common;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class ReferenceServiceFixture
    {
        [Test]
        public void Get_ShouldReturnAllEntities()
        {
            // Setup Dependencies
            var occ1 = new RefOccupation { Code = "OCC1", Description = "Occupation 1" };
            var occ2 = new RefOccupation { Code = "OCC2", Description = "Occupation 2" };

            var referenceRepository = new MockReferenceRepository()
                .With(occ1)
                .With(occ2);

            // Call Get
            var returnedEntities = new ReferenceService(referenceRepository).Get<RefOccupation>();

            // Verify result
            returnedEntities.ToList().AssertCollectionContainsItems(occ1, occ2);
        }

        [Test]
        public void GetByCode_ShouldThrowExceptionIfEntityNotFound()
        {
            // Setup Dependencies
            var referenceRepository = new MockReferenceRepository();

            // Call GetByCode
            var exception = Assert.Throws<EntityNotFoundException<RefOccupation>>(() =>
            {
                new ReferenceService(referenceRepository).GetByCode<RefOccupation>("IDONTEXIST");
            });

            // Verify result
            Assert.AreEqual("IDONTEXIST", exception.Entity.Code);
        }

        [Test]
        public void GetByCode_ShouldReturnExpectedEntity()
        {
            // Setup dependencies
            var occ1 = new RefOccupation { Code = "OCC1", Description = "Occupation 1" };
            var occ2 = new RefOccupation { Code = "OCC2", Description = "Occupation 2" };

            var referenceRepository = new MockReferenceRepository()
                .With(occ1)
                .With(occ2);

            // Call GetByCode
            var returnedEntity = new ReferenceService(referenceRepository).GetByCode<RefOccupation>("OCC1");

            // Verify result
            Assert.AreEqual(occ1, returnedEntity);
        }
    }
}
