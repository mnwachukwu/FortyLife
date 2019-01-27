using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class CategoryProductsSearchBody
    {
        public string Sort { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public List<Filter> Filters { get; set; }
    }

    public class Filter
    {
        public enum FilterName
        {
            Null = 0,
            ProductName,
            SetName
        }

        public string Name { get; set; }

        public List<string> Values { get; set; }
    }
}
