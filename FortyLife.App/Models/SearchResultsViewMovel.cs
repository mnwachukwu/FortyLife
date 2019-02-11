using FortyLife.DataAccess.Scryfall;

namespace FortyLife.App.Models
{
    public class SearchResultsViewMovel
    {
        public ScryfallList<Card> Results { get; set; }
        public string NameSearch { get; set; }
    }
}