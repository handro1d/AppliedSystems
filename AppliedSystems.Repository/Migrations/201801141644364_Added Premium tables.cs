namespace AppliedSystems.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPremiumtables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InsurancePolicy", "IsApplicationDenied");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InsurancePolicy", "IsApplicationDenied", c => c.Boolean(nullable: false));
        }
    }
}
