using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class MarketPriceResults
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<Price> Results { get; set; }
    }

    public class Price
    {
        public int ProductId { get; set; }

        public double? LowPrice { get; set; }

        public double? MidPrice { get; set; }

        public double? HighPrice { get; set; }

        public double? MarketPrice { get; set; }

        public double? DirectLowPrice { get; set; }

        public string SubTypeName { get; set; }
    }
}
