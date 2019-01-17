using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Data.Scryfall
{
    public class ScryfallList
    {
        public string Object { get; set; }

        public int TotalCards { get; set; }

        public bool HasMore { get; set; }

        public List<Card> Data { get; set; }
    }
}
