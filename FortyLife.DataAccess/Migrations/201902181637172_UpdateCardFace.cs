namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CardFaces");
            AddColumn("dbo.CardFaces", "CardFaceId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CardFaces", "CardFaceId");
            DropColumn("dbo.CardFaces", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardFaces", "Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.CardFaces");
            DropColumn("dbo.CardFaces", "CardFaceId");
            AddPrimaryKey("dbo.CardFaces", "Id");
        }
    }
}
