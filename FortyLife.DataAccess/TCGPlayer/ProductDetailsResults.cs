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

    public class ProductDetail
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string CleanName { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int GroupId { get; set; }

        public string Url { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
