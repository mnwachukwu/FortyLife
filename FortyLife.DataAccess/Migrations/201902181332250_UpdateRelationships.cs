namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationships : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cards", name: "CardFacesId", newName: "CardFaces_Id");
            RenameIndex(table: "dbo.Cards", name: "IX_CardFacesId", newName: "IX_CardFaces_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cards", name: "IX_CardFaces_Id", newName: "IX_CardFacesId");
            RenameColumn(table: "dbo.Cards", name: "CardFaces_Id", newName: "CardFacesId");
        }
    }
}
