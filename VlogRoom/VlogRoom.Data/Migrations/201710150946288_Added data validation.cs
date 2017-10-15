namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddatavalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "RoomName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Videos", "ServiceVideoId", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "ServiceListItemId", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "Title", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Videos", "Description", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Videos", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "ImageUrl", c => c.String());
            AlterColumn("dbo.Videos", "Description", c => c.String());
            AlterColumn("dbo.Videos", "Title", c => c.String());
            AlterColumn("dbo.Videos", "ServiceListItemId", c => c.String());
            AlterColumn("dbo.Videos", "ServiceVideoId", c => c.String());
            AlterColumn("dbo.Users", "RoomName", c => c.String());
        }
    }
}
