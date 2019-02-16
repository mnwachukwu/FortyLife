using System.Data.Entity;
using FortyLife.DataAccess.TCGPlayer;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.DataAccess
{
    public class FortyLifeDbContext : DbContext
    {
        /// <summary>
        /// Db Context for the Forty Life ApplicationUser objects.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Db Context for Forty Life Collection objects. Decks are Collections, too.
        /// </summary>
        public DbSet<Collection> Collections { get; set; }

        /// <summary>
        /// Db Context for the Scryfall Card objects.
        /// </summary>
        public DbSet<Scryfall.Card> Cards { get; set; }

        /// <summary>
        /// Db Context for the TCGPlayer Price objects.
        /// </summary>
        public DbSet<Price> Prices { get; set; }

        /// <summary>
        /// Db Context for the TCGPlayer Product Ids objects.
        /// </summary>
        public DbSet<CardProductId> CardProductIds { get; set; }

        /// <summary>
        /// Db Context for the TCGPlayer Product Details objects.
        /// </summary>
        public DbSet<ProductDetail> ProductDetails { get; set; }
    }
}
