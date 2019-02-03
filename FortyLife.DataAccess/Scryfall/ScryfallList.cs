using System.Collections.Generic;

namespace FortyLife.DataAccess.Scryfall
{
    public class ScryfallList<T>
    {
        public string Object { get; set; }

        public int TotalCards { get; set; }

        public bool HasMore { get; set; }

        public List<T> Data { get; set; }
    }
}
