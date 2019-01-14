using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FortyLife.Data.Scryfall;

namespace FortyLife.Models
{
    public class SearchResultsViewMovel
    {
        public ScryfallList Results { get; set; }
        public string NameSearch { get; set; }
    }
}