namespace PHSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertedChnagesMadeToTeamAndLogModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LogModels", "TeamId", "dbo.TeamModels");
            DropPrimaryKey("dbo.LogModels");
            AddPrimaryKey("dbo.LogModels", "TeamId");
            AddForeignKey("dbo.LogModels", "TeamId", "dbo.TeamModels", "TeamId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogModels", "TeamId", "dbo.TeamModels");
            DropPrimaryKey("dbo.LogModels");
            AddPrimaryKey("dbo.LogModels", new[] { "TeamId", "SeasonId" });
            AddForeignKey("dbo.LogModels", "TeamId", "dbo.TeamModels", "TeamId", cascadeDelete: true);
        }
    }
}
