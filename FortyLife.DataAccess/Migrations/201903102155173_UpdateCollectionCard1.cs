namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectionCard1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CollectionCards", name: "Collection_CollectionId", newName: "CollectionId");
            RenameIndex(table: "dbo.CollectionCards", name: "IX_Collection_CollectionId", newName: "IX_CollectionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CollectionCards", name: "IX_CollectionId", newName: "IX_Collection_CollectionId");
            RenameColumn(table: "dbo.CollectionCards", name: "CollectionId", newName: "Collection_CollectionId");
        }
    }
}
