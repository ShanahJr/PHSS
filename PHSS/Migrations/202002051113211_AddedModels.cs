namespace PHSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeGroupModels",
                c => new
                    {
                        AgeGroupID = c.Int(nullable: false, identity: true),
                        AgeGroupNameName = c.String(),
                    })
                .PrimaryKey(t => t.AgeGroupID);
            
            CreateTable(
                "dbo.DivisionModels",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId);

            CreateTable(
                "dbo.FixtureModels",
                c => new
                {
                    Team1Id = c.Int(nullable: false),
                    Team2Id = c.Int(nullable: false),
                    FixtureId = c.Int(nullable: false, identity: true),
                    FixtureName = c.String(),
                    Matchdate = c.DateTime(nullable: false),
                    Executed = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.TeamModels", t => t.Team1Id, cascadeDelete: false)
                .ForeignKey("dbo.TeamModels", t => t.Team2Id, cascadeDelete: false)
                .Index(t => t.Team1Id)
                .Index(t => t.Team2Id);
            
            CreateTable(
                "dbo.TeamModels",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        SchoolId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                        UnderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.AgeGroupModels", t => t.UnderId, cascadeDelete: false)
                .ForeignKey("dbo.DivisionModels", t => t.DivisionId, cascadeDelete: false)
                .ForeignKey("dbo.SchoolModels", t => t.SchoolId, cascadeDelete: false)
                .Index(t => t.SchoolId)
                .Index(t => t.DivisionId)
                .Index(t => t.UnderId);
            
            CreateTable(
                "dbo.PlayerModels",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ForeNames = c.String(),
                        Position = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.TeamModels", t => t.Team_TeamId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.SchoolModels",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            CreateTable(
                "dbo.LogModels",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                        Played = c.Int(nullable: false),
                        Win = c.Int(nullable: false),
                        Draw = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        GoalsFor = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                        GoalDifference = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        LogStanding = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.SeasonId })
                .ForeignKey("dbo.SeasonModels", t => t.SeasonId, cascadeDelete: true)
                .ForeignKey("dbo.TeamModels", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.SeasonModels",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        SeasonName = c.String(),
                    })
                .PrimaryKey(t => t.SeasonId);
            
            CreateTable(
                "dbo.ResultModels",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        FixtureId = c.Int(nullable: false),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.FixtureModels", t => t.FixtureId, cascadeDelete: true)
                .Index(t => t.FixtureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultModels", "FixtureId", "dbo.FixtureModels");
            DropForeignKey("dbo.FixtureModels", "Team2Id", "dbo.TeamModels");
            DropForeignKey("dbo.FixtureModels", "Team1Id", "dbo.TeamModels");
            DropForeignKey("dbo.LogModels", "TeamId", "dbo.TeamModels");
            DropForeignKey("dbo.LogModels", "SeasonId", "dbo.SeasonModels");
            DropForeignKey("dbo.TeamModels", "SchoolId", "dbo.SchoolModels");
            DropForeignKey("dbo.PlayerModels", "Team_TeamId", "dbo.TeamModels");
            DropForeignKey("dbo.TeamModels", "DivisionId", "dbo.DivisionModels");
            DropForeignKey("dbo.TeamModels", "UnderId", "dbo.AgeGroupModels");
            DropIndex("dbo.ResultModels", new[] { "FixtureId" });
            DropIndex("dbo.LogModels", new[] { "SeasonId" });
            DropIndex("dbo.LogModels", new[] { "TeamId" });
            DropIndex("dbo.PlayerModels", new[] { "Team_TeamId" });
            DropIndex("dbo.TeamModels", new[] { "UnderId" });
            DropIndex("dbo.TeamModels", new[] { "DivisionId" });
            DropIndex("dbo.TeamModels", new[] { "SchoolId" });
            DropIndex("dbo.FixtureModels", new[] { "Team2Id" });
            DropIndex("dbo.FixtureModels", new[] { "Team1Id" });
            DropTable("dbo.ResultModels");
            DropTable("dbo.SeasonModels");
            DropTable("dbo.LogModels");
            DropTable("dbo.SchoolModels");
            DropTable("dbo.PlayerModels");
            DropTable("dbo.TeamModels");
            DropTable("dbo.FixtureModels");
            DropTable("dbo.DivisionModels");
            DropTable("dbo.AgeGroupModels");
        }
    }
}
