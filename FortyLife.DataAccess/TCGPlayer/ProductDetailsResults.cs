using System.Collections.Generic;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class ProductDetailsResults
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<ProductDetail> Results { get; set; }
    }
}
