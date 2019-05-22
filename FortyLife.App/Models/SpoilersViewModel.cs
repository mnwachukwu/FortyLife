using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.App.Models
{
    public class SpoilersViewModel
    {
        public string Set { get; set; }

        public string SetName { get; set; }
        
        public List<Card> Cards { get; set; }

        public ScryfallList<Set> Sets { get; set; }
    }
}