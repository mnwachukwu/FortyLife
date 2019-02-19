namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CardFaces");
            AddColumn("dbo.CardFaces", "CardFaceId", c => c.Int(nullable: false, identity: true));
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
