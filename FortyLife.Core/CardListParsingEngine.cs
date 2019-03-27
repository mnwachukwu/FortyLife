using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyLife.DataAccess;
using FortyLife.DataAccess.UserAccount;
using FortyLife.DataAccess.Scryfall;

namespace FortyLife.Core
{
    public static class CardListParsingEngine
    {
        public enum CardListFormat
        {
            FortyLife = 0
        }

        public static List<CollectionCard> ParseCardList(string rawList, out string error)
        {
            error = "";
            var cardList = new List<CollectionCard>();

            if (string.IsNullOrEmpty(rawList))
            {
                error = "The card list is empty!";
                return cardList;
            }

            var rawLines = rawList.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (rawLines.Length <= 0)
            {
                error = "The card list is empty!";
                return cardList;
            }

            foreach (var line in rawLines)
            {
                var cardData = line.Split(' '); // format: [0x] cardname [(setcode) *F* *CMDR*], [] indicated optional
                var count = 1;
                var setCode = "";
                var cardName = "";

                // ending character indicated the first value may be numeric
                if (cardData[0].EndsWith("x") || cardData[0].EndsWith("X") || cardData[0].EndsWith("*")) // because people like star, too
                {
                    if (int.TryParse(cardData[0].Substring(0, cardData[0].Length - 1), out count))
                    {
                        cardName = line.Replace(cardData[0], string.Empty).Replace("*F*", string.Empty).Replace("*CMDR*", string.Empty).Trim();

                        if (count <= 0)
                        {
                            count = 1;
                        }
                    }
                }

                // this means that they did not use a count, so just trim off the rest of the string
                if (string.IsNullOrEmpty(cardName))
                {
                    cardName = line.Replace("*F*", string.Empty).Replace("*CMDR*", string.Empty).Trim();
                }

                // trim the set code off the card name
                if (cardName.Contains("(") && cardName.Contains(")"))
                {
                    var start = cardName.IndexOf("(", StringComparison.Ordinal);
                    var end = cardName.IndexOf(")", StringComparison.Ordinal);
                    var length = end - start + 1;

                    cardName = cardName.Remove(start, length).Trim();
                }

                // check to see if they used a set
                setCode = ParseSetCode(cardData.FirstOrDefault(i => i.StartsWith("(") && i.EndsWith(")")));

                // or foil indicator
                var foil = cardData.Contains("*F*");

                // or is the list's commander
                var isCommander = cardData.Contains("*CMDR*");

                cardList.Add(new CollectionCard
                {
                    Foil = foil,
                    Name = cardName,
                    SetCode = setCode,
                    Commander = isCommander,
                    Count = count
                });
            }

            return cardList;
        }

        public static string GetCardList(List<CollectionCard> list, CardListFormat format = CardListFormat.FortyLife)
        {
            var builder = new StringBuilder();

            foreach (var card in list)
            {
                builder.Append($"{card.Count}x {card.Name} ");

                if (!string.IsNullOrEmpty(card.SetCode))
                {
                    builder.Append($"({card.SetCode}) ");
                }

                if (card.Foil)
                {
                    builder.Append("*F* ");
                }

                if (card.Commander)
                {
                    builder.Append("*CMDR*");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        public static string ParseSetCode(string rawSetCode)
        {
            if (rawSetCode == null) return string.Empty;

            return rawSetCode.Replace("(", string.Empty).Replace(")", string.Empty);
        }

        public static bool VerifyCardList(List<CollectionCard> list, out List<CollectionCard> verifiedList, out string error)
        {
            var requestEngine = new ScryfallRequestEngine();
            error = "";
            var errorLine = 0;
            verifiedList = new List<CollectionCard>();

            foreach (var card in list)
            {
                errorLine++;

                if (requestEngine.FirstCardFromSearch(card.Name, card.SetCode) == null)
                {
                    error = $"Error processing the Card named {card.Name}" + (!string.IsNullOrEmpty(card.SetCode)
                        ? $" with Set Code {card.SetCode}."
                        : ".");
                    error += $" (Line {errorLine})";

                    return false;
                }

                card.Name = requestEngine.FirstCardFromSearch(card.Name, card.SetCode).Name;
            }

            if (list.Count(i => i.Commander) > 1)
            {
                var commanders = list.Where(i => i.Commander).ToList();

                foreach (var commander in commanders)
                {
                    var card = requestEngine.FirstCardFromSearch(commander.Name, commander.SetCode);
                    if (!card.TypeLine.Contains("Legendary") && !card.TypeLine.Contains("Creature") || !card.OracleText.Contains("can be your commander"))
                    {
                        error =
                            "Your Commander has to be a Legendary Creature or a card with \"can be your commander\" in its rules text.";
                        return false;
                    }
                }

                if (!commanders.All(i => i.Name.Contains("Kenrith")))
                {
                    foreach (var commander in commanders)
                    {
                        var card = requestEngine.FirstCardFromSearch(commander.Name);
                        if (!card.OracleText.Contains("Partner") && !card.OracleText.Contains("Partner with"))
                        {
                            error = "You can't have more than one commander in your list.";
                            return false;
                        }
                    }
                }
            }

            verifiedList = list;
            return true;
        }
    }
}
