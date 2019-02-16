using System.Data.Entity.Migrations;

namespace FortyLife.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FortyLifeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FortyLife.DataAccess.FortyLifeDbContext";
        }

        protected override void Seed(FortyLifeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
