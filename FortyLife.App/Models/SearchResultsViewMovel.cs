using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.Models
{
    public class SearchResultsViewMovel
    {
        public ScryfallList<Card> Results { get; set; }
        public string NameSearch { get; set; }
    }
}