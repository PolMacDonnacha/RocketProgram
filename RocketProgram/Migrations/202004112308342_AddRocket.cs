namespace RocketProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRocket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rockets", "MissionID", "dbo.Missions");
            DropIndex("dbo.Rockets", new[] { "MissionID" });
            CreateIndex("dbo.Missions", "RocketID");
            AddForeignKey("dbo.Missions", "RocketID", "dbo.Rockets", "RocketID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Missions", "RocketID", "dbo.Rockets");
            DropIndex("dbo.Missions", new[] { "RocketID" });
            CreateIndex("dbo.Rockets", "MissionID");
            AddForeignKey("dbo.Rockets", "MissionID", "dbo.Missions", "MissionID", cascadeDelete: true);
        }
    }
}
