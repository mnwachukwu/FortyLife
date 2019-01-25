using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Data.TCGPlayer
{
    public class CategoryProductsResult
    {
        public int TotalItems { get; set; }

        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<int> Results { get; set; }
    }
}
