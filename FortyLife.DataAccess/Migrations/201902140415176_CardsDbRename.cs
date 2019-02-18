namespace FortyLife.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CardsDbRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Card", newName: "Cards");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Cards", newName: "Card");
        }
    }
}
