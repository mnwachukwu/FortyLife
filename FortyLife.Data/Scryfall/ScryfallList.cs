using System.Collections.Generic;

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
