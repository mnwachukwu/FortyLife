namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollections2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Collections", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Collections", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Collections", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Collections", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Collections", "ApplicationUserId");
            AddForeignKey("dbo.Collections", "ApplicationUserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collections", "ApplicationUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Collections", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Collections", "ApplicationUserId", c => c.Int());
            RenameColumn(table: "dbo.Collections", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Collections", "ApplicationUser_Id");
            AddForeignKey("dbo.Collections", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
