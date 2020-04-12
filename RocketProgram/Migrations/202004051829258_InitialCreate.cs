namespace RocketProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        MissionID = c.Int(nullable: false, identity: true),
                        MissionName = c.String(),
                        MissionDescription = c.String(),
                        LaunchDate = c.DateTime(nullable: false),
                        LaunchSite = c.String(),
                        RocketID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MissionID);
            
            CreateTable(
                "dbo.Payloads",
                c => new
                    {
                        PayloadID = c.Int(nullable: false, identity: true),
                        PayloadName = c.String(),
                        NumberOfSatellites = c.Int(nullable: false),
                        Description = c.String(),
                        Manufacturer = c.String(),
                        DestinationOrbit = c.String(),
                        MissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayloadID)
                .ForeignKey("dbo.Missions", t => t.MissionID, cascadeDelete: true)
                .Index(t => t.MissionID);
            
            CreateTable(
                "dbo.Rockets",
                c => new
                    {
                        RocketID = c.Int(nullable: false, identity: true),
                        RocketName = c.String(),
                        Manufacturer = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        CountryOfOrigin = c.String(),
                        MissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RocketID)
                .ForeignKey("dbo.Missions", t => t.MissionID, cascadeDelete: true)
                .Index(t => t.MissionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rockets", "MissionID", "dbo.Missions");
            DropForeignKey("dbo.Payloads", "MissionID", "dbo.Missions");
            DropIndex("dbo.Rockets", new[] { "MissionID" });
            DropIndex("dbo.Payloads", new[] { "MissionID" });
            DropTable("dbo.Rockets");
            DropTable("dbo.Payloads");
            DropTable("dbo.Missions");
        }
    }
}
