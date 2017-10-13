namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeduserroomname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RoomName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RoomName");
        }
    }
}
