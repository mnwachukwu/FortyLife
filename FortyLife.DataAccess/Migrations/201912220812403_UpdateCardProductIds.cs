namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardProductIds : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CardProductIds", "CacheDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardProductIds", "CacheDate", c => c.DateTime(nullable: false));
        }
    }
}
