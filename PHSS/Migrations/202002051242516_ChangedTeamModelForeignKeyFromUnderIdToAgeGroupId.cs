namespace PHSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTeamModelForeignKeyFromUnderIdToAgeGroupId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TeamModels", name: "UnderId", newName: "AgeGroupId");
            RenameIndex(table: "dbo.TeamModels", name: "IX_UnderId", newName: "IX_AgeGroupId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TeamModels", name: "IX_AgeGroupId", newName: "IX_UnderId");
            RenameColumn(table: "dbo.TeamModels", name: "AgeGroupId", newName: "UnderId");
        }
    }
}
