namespace PHSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAgeGroupName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgeGroupModels", "AgeGroupName", c => c.String());
            DropColumn("dbo.AgeGroupModels", "AgeGroupNameName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgeGroupModels", "AgeGroupNameName", c => c.String());
            DropColumn("dbo.AgeGroupModels", "AgeGroupName");
        }
    }
}
