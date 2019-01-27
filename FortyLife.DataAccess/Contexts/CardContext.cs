using System.Data.Entity;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.DataAccess.Contexts
{
    class CardContext : DbContext
    {
        public CardContext() : base()
        {

        }

        public DbSet<Card> Cards { get; set; }
    }
}
