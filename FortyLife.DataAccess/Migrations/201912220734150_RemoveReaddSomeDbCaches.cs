namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveReaddSomeDbCaches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardProductIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardName = c.String(),
                        SetName = c.String(),
                        ProductId = c.Int(nullable: false),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CardProductIds");
        }
    }
}
