namespace RocketProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPayloadImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payloads", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payloads", "Image");
        }
    }
}
