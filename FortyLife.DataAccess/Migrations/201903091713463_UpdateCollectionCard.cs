namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectionCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectionCards", "Commander", c => c.Boolean(nullable: false));
            AddColumn("dbo.CollectionCards", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollectionCards", "Count");
            DropColumn("dbo.CollectionCards", "Commander");
        }
    }
}
