using System;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class Price
    {
        public int PriceId { get; set; }

        public int ProductId { get; set; }

        public double? LowPrice { get; set; }

        public double? MidPrice { get; set; }

        public double? HighPrice { get; set; }

        public double? MarketPrice { get; set; }

        public double? DirectLowPrice { get; set; }

        public string SubTypeName { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
