using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Repository
{
    public class AppliedSystemsContext : DbContext
    {
        public AppliedSystemsContext()
            : base("AppliedSystemsContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<InsurancePolicy> Policies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppliedSystemsContext, Configuration>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Create UserRoles link table
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .Map(m =>
                {
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("UserId");
                    m.ToTable("UserRoles");
                });

            // Create RolePermissions link table
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .Map(m =>
                {
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("PermissionId");
                    m.ToTable("RolePermissions");
                });
        }
    }
}
