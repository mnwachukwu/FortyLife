using System.Collections.Generic;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class MarketPriceResults
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<Price> Results { get; set; }
    }
}
