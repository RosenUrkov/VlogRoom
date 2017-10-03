namespace VlogRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomeExtensions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Users", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Users", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Users", "IsDeleted");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "IsDeleted" });
            DropIndex("dbo.Users", "UserNameIndex");
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "DeletedOn");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Users", "ModifiedOn");
            DropColumn("dbo.Users", "CreatedOn");
        }
    }
}
