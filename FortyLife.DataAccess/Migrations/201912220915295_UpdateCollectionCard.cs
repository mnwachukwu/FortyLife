namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectionCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectionCards", "SetName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollectionCards", "SetName");
        }
    }
}
