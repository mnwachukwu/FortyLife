namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "CreateDate");
        }
    }
}
