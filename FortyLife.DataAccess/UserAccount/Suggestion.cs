using System;

namespace FortyLife.DataAccess.UserAccount
{
    public class Suggestion
    {
        public int SuggestionId { get; set; }

        public int SuggesterId { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
