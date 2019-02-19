namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CardFaces");
            AddColumn("dbo.CardFaces", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CardFaces", "Id");
            DropColumn("dbo.CardFaces", "CardFaceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardFaces", "CardFaceId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.CardFaces");
            DropColumn("dbo.CardFaces", "Id");
            AddPrimaryKey("dbo.CardFaces", "CardFaceId");
        }
    }
}
