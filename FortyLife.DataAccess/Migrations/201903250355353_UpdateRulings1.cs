namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRulings1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Rulings");
            AlterColumn("dbo.Rulings", "OracleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Rulings", "OracleId");
            DropColumn("dbo.Rulings", "RulingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rulings", "RulingId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Rulings");
            AlterColumn("dbo.Rulings", "OracleId", c => c.String());
            AddPrimaryKey("dbo.Rulings", "RulingId");
        }
    }
}
