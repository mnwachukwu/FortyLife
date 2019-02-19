namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardFaces", "ColorsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CardFaces", "ColorsString");
        }
    }
}
