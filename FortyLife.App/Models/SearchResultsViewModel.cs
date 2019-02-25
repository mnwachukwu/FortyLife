using FortyLife.DataAccess.Scryfall;

namespace FortyLife.App.Models
{
    public class SearchResultsViewModel
    {
        public ScryfallList<Card> Results { get; set; }
        public string NameSearch { get; set; }
    }
}