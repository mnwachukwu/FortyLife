using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyLife.Data.Scryfall;

namespace FortyLife.Database
{
    class CardContext : DbContext
    {
        public CardContext() : base()
        {

        }

        public DbSet<Card> Cards { get; set; }
    }
}
