using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AppliedSystems.Domain;
using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppliedSystemsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppliedSystemsContext context)
        {
            //  This method will be called after migrating to the latest version.
            AddToContextIfNotExisting(context, GetOccupationsToAdd());
        }

        private static IEnumerable<RefOccupation> GetOccupationsToAdd()
        {
            return new List<RefOccupation>
            {
                new RefOccupation{ Description = "Accountant", Code = "ACCOUNT" },
                new RefOccupation{ Description = "Chauffeur", Code = "CHAUFF" },
                new RefOccupation{ Description = "Farmer", Code = "FARMER" },
                new RefOccupation{ Description = "Barrista", Code = "BAR" }
            };
        }

        private static void AddToContextIfNotExisting<TReferenceEntity>(AppliedSystemsContext context, IEnumerable<TReferenceEntity> entities)
            where TReferenceEntity : class, IReferenceEntity
        {
            foreach (var entity in entities)
            {
                if (!context.Set<TReferenceEntity>().Any(e =>
                    e.Description.Equals(entity.Description, StringComparison.InvariantCultureIgnoreCase)))
                {
                    context.Set<TReferenceEntity>().Add(entity);
                }
            }
        }
    }
}
