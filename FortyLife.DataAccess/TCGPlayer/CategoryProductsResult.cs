using System.Collections.Generic;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class CategoryProductsResult
    {
        public int TotalItems { get; set; }

        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<int> Results { get; set; }
    }
}
