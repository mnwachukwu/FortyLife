using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.App.Models
{
    public class SpoilersViewModel
    {
        public Set Set { get; set; }

        public List<Card> Cards { get; set; }

        public ScryfallList<Set> Sets { get; set; }

        public int SortId { get; set; }

        public readonly List<SelectListItem> Sort = new List<SelectListItem>
        {
            new SelectListItem {Text = "Collector Number", Value = "0"},
            new SelectListItem {Text = "Rarity", Value = "1"},
            new SelectListItem {Text = "Name", Value = "2"},
            new SelectListItem {Text = "Converted Mana Cost", Value = "3"}
        };

        public static readonly string[] RarityOrder =
        {
            "mythic",
            "rare",
            "uncommon",
            "common"
        };
    }
}