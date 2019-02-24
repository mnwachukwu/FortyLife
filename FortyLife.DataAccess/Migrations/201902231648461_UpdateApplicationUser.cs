namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "ActivationKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "ActivationKey");
        }
    }
}
