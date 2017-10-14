namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeddurationfromvideomodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Video_Id", "dbo.Videos");
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropIndex("dbo.Comments", new[] { "Video_Id" });
            DropColumn("dbo.Videos", "Duration");
            DropTable("dbo.Comments");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Videos", "Duration", c => c.String());
            CreateIndex("dbo.Comments", "Video_Id");
            CreateIndex("dbo.Comments", "IsDeleted");
            AddForeignKey("dbo.Comments", "Video_Id", "dbo.Videos", "Id");
        }
    }
}
