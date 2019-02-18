namespace FortyLife.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCacheDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "CacheDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Prices", "CacheDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prices", "CacheDate");
            DropColumn("dbo.Cards", "CacheDate");
        }
    }
}
