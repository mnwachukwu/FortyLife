using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class ProductDetailsResults
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<ProductDetail> Results { get; set; }
    }
}
