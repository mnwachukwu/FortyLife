namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePrices : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Prices");
            AlterColumn("dbo.Prices", "SubTypeName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Prices", new[] { "ProductId", "SubTypeName" });
            DropColumn("dbo.Prices", "PriceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prices", "PriceId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Prices");
            AlterColumn("dbo.Prices", "SubTypeName", c => c.String());
            AddPrimaryKey("dbo.Prices", "PriceId");
        }
    }
}
