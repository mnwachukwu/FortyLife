namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollections1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Suggestions", name: "Deck_CollectionId", newName: "Collection_CollectionId");
            RenameIndex(table: "dbo.Suggestions", name: "IX_Deck_CollectionId", newName: "IX_Collection_CollectionId");
            AddColumn("dbo.Suggestions", "SuggesterId", c => c.Int(nullable: false));
            DropColumn("dbo.Suggestions", "SuggesterName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suggestions", "SuggesterName", c => c.String());
            DropColumn("dbo.Suggestions", "SuggesterId");
            RenameIndex(table: "dbo.Suggestions", name: "IX_Collection_CollectionId", newName: "IX_Deck_CollectionId");
            RenameColumn(table: "dbo.Suggestions", name: "Collection_CollectionId", newName: "Deck_CollectionId");
        }
    }
}
