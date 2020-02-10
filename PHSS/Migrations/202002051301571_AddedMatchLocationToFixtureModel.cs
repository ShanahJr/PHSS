namespace PHSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMatchLocationToFixtureModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FixtureModels", "MatchLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FixtureModels", "MatchLocation");
        }
    }
}
