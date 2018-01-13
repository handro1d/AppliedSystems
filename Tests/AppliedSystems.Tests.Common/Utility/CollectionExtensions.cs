using System.Collections.Generic;
using NUnit.Framework;

namespace AppliedSystems.Tests.Common
{
    public static class CollectionExtensions
    {
        public static void AssertCollectionContainsItems<TEntity>(this IList<TEntity> collection, params TEntity[] entities)
        {
            Assert.AreEqual(entities.Length, collection.Count);

            foreach (TEntity entity in entities)
            {
                CollectionAssert.Contains(collection, entity);
            }
        }
    }
}
