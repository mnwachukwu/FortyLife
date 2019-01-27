using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.Scryfall
{
    public class Set
    {
        public string Object { get; set; }

        public string Id { get; set; }

        public string Code { get; set; }

        public string MtgoCode { get; set; }

        public int TcgPlayerId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string ScryfallUri { get; set; }

        public string SearchUri { get; set; }

        public DateTime ReleasedAt { get; set; }

        public string SetType { get; set; }

        public int CardCount { get; set; }

        public bool Digital { get; set; }

        public bool FoilOnly { get; set; }

        public string BlockCode { get; set; }

        public string Block { get; set; }

        public string IconSvgUri { get; set; }
    }

    public class SetName
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Rarity { get; set; }
    }
}
