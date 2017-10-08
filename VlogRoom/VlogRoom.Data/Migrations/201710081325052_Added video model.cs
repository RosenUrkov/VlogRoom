namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedvideomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceVideoId = c.String(),
                        ServiceListItemId = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "User_Id", "dbo.Users");
            DropIndex("dbo.Videos", new[] { "User_Id" });
            DropIndex("dbo.Videos", new[] { "IsDeleted" });
            DropTable("dbo.Videos");
        }
    }
}
