using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Core.UserAccount
{
    public class Collection
    {
        public int CardCount => CardList?.Count ?? 0;

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Card> CardList { get; set; }

        public double TcgMidValue { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastEditDate { get; set; }

        // TODO: when implemented, make very sure it cannot be abused! (one view, per IP, per day)
        public int Views { get; set; }
    }
}
