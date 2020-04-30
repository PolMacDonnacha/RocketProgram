namespace RocketProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMissionID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rockets", "MissionID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rockets", "MissionID", c => c.Int(nullable: false));
        }
    }
}
