namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRulings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rulings",
                c => new
                    {
                        RulingId = c.Int(nullable: false, identity: true),
                        RulingUri = c.String(),
                        Object = c.String(),
                        OracleId = c.String(),
                        Source = c.String(),
                        PublishedAt = c.DateTime(),
                        Comment = c.String(),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RulingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rulings");
        }
    }
}
