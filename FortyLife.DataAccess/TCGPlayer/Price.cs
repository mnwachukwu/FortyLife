using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class Price
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }

        public double? LowPrice { get; set; }

        public double? MidPrice { get; set; }

        public double? HighPrice { get; set; }

        public double? MarketPrice { get; set; }

        public double? DirectLowPrice { get; set; }

        [Key, Column(Order = 1)]
        public string SubTypeName { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
