namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectionCards : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CollectionCards");
            AddPrimaryKey("dbo.CollectionCards", new[] { "CollectionId", "Name", "SetCode", "Foil" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CollectionCards");
            AddPrimaryKey("dbo.CollectionCards", new[] { "CollectionId", "Name", "SetCode" });
        }
    }
}
