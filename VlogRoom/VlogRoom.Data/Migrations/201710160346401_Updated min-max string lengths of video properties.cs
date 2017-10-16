namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedminmaxstringlengthsofvideoproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Videos", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Videos", "Description", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "Description", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Videos", "Title", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
