namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedvideocomments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerUsername = c.String(),
                        Content = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Video_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Videos", t => t.Video_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Video_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Video_Id", "dbo.Videos");
            DropIndex("dbo.Comments", new[] { "Video_Id" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropTable("dbo.Comments");
        }
    }
}
