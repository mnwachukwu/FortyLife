using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.Core
{
    public static class CardListParsingEngine
    {
        public static List<CollectionCard> ParseCardList(string rawList, out string error)
        {
            var rawLines = rawList.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (rawLines.Length <= 0)
            {
                error = "The card list is empty!";
                return new List<CollectionCard>();
            }

            // TODO: finish the parser
            return new List<CollectionCard>();
        }
    }
}
