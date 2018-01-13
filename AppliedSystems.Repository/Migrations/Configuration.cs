using System.Data.Entity.Migrations;

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
            // TODO: Add data to include in seed method
        }
    }
}
