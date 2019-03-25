namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollections3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectionCards", "CollectionId", "dbo.Collections");
            DropIndex("dbo.CollectionCards", new[] { "CollectionId" });
            DropPrimaryKey("dbo.CollectionCards");
            AlterColumn("dbo.CollectionCards", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CollectionCards", "SetCode", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CollectionCards", "CollectionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CollectionCards", new[] { "CollectionId", "Name", "SetCode" });
            CreateIndex("dbo.CollectionCards", "CollectionId");
            AddForeignKey("dbo.CollectionCards", "CollectionId", "dbo.Collections", "CollectionId", cascadeDelete: true);
            DropColumn("dbo.CollectionCards", "CollectionCardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectionCards", "CollectionCardId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.CollectionCards", "CollectionId", "dbo.Collections");
            DropIndex("dbo.CollectionCards", new[] { "CollectionId" });
            DropPrimaryKey("dbo.CollectionCards");
            AlterColumn("dbo.CollectionCards", "CollectionId", c => c.Int());
            AlterColumn("dbo.CollectionCards", "SetCode", c => c.String());
            AlterColumn("dbo.CollectionCards", "Name", c => c.String());
            AddPrimaryKey("dbo.CollectionCards", "CollectionCardId");
            CreateIndex("dbo.CollectionCards", "CollectionId");
            AddForeignKey("dbo.CollectionCards", "CollectionId", "dbo.Collections", "CollectionId");
        }
    }
}
