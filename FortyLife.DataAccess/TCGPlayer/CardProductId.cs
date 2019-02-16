using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class CardProductId
    {
        public int Id { get; set; }

        public string CardName { get; set; }

        public string SetName { get; set; }

        public int ProductId { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
