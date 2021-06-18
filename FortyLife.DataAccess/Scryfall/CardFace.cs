using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.DataAccess.Scryfall
{
    public class CardFace
    {
        public int CardFaceId { get; set; }

        public string Object { get; set; }

        public string Name { get; set; }

        public string ManaCost { get; set; }

        public string TypeLine { get; set; }

        public string OracleText { get; set; }

        public string ColorsString { get; set; }

        public List<string> Colors { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string FlavorText { get; set; }

        public string Artist { get; set; }

        public string IllustrationId { get; set; }

        public ImageUris ImageUris { get; set; }

        public string Loyalty { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
