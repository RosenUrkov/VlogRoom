namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatevideoandusermodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserUser1",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        User_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 })
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            AddColumn("dbo.Videos", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Videos", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUser1", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.UserUser1", "User_Id", "dbo.Users");
            DropIndex("dbo.UserUser1", new[] { "User_Id1" });
            DropIndex("dbo.UserUser1", new[] { "User_Id" });
            DropColumn("dbo.Videos", "Duration");
            DropColumn("dbo.Videos", "Views");
            DropTable("dbo.UserUser1");
        }
    }
}
