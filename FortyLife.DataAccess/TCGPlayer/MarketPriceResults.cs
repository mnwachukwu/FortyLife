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
}
