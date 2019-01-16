using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Core
{
    public static class CardDataFormatter
    {
        // used for sorting colors from scryfall
        public static string WubrgOrder = "WUBRG";

        public static Dictionary<string, string> WubrgHtmlColors = new Dictionary<string, string>
        {
            {"White", "#ffff99"},
            {"Blue", "#3366ff"},
            {"Black", "#161519"},
            {"Red", "#ff5050"},
            {"Green", "#33cc33"},
            {"Colorless", "#d2d7dd" }
        };

        public static string FormatColor(string color)
        {
            return FormatColor(new List<string> { color });
        }

        public static string FormatColor(List<string> colors)
        {
            if (colors == null || colors.Count == 0)
                return "Colorless";

            var sb = new StringBuilder();
            var sortedColors = colors.OrderBy(i => WubrgOrder.IndexOf(i, StringComparison.Ordinal));

            foreach (var color in sortedColors)
            {
                switch (color)
                {
                    case "W":
                        sb.Append("White ");
                        break;

                    case "U":
                        sb.Append("Blue ");
                        break;

                    case "B":
                        sb.Append("Black ");
                        break;

                    case "R":
                        sb.Append("Red ");
                        break;

                    case "G":
                        sb.Append("Green ");
                        break;
                }
            }

            return sb.ToString().Remove(sb.Length - 1);
        }

        public static string RenderSymbols(string originalText)
        {
            if (originalText == null)
                return string.Empty;

            // Mana costs
            var newText = originalText.Replace("{W}", "<i class=\"ms ms-w ms-cost ms-shadow\" title=\"one white mana\"></i>");
            newText = newText.Replace("{U}", "<i class=\"ms ms-u ms-cost ms-shadow\" title=\"one blue mana\"></i>");
            newText = newText.Replace("{B}", "<i class=\"ms ms-b ms-cost ms-shadow\" title=\"one black mana\"></i>");
            newText = newText.Replace("{R}", "<i class=\"ms ms-r ms-cost ms-shadow\" title=\"one red mana\"></i>");
            newText = newText.Replace("{G}", "<i class=\"ms ms-g ms-cost ms-shadow\" title=\"one green mana\"></i>");
            newText = newText.Replace("{C}", "<i class=\"ms ms-c ms-cost ms-shadow\" title=\"one colorless mana\"></i>");
            newText = newText.Replace("{0}", "<i class=\"ms ms-0 ms-cost ms-shadow\" title=\"zero mana\"></i>");

            newText = newText.Replace("{X}", "<i class=\"ms ms-x ms-cost ms-shadow\" title=\"X generic mana\"></i>");
            newText = newText.Replace("{Y}", "<i class=\"ms ms-y ms-cost ms-shadow\" title=\"Y generic mana\"></i>");
            newText = newText.Replace("{Z}", "<i class=\"ms ms-z ms-cost ms-shadow\" title=\"Z generic mana\"></i>");

            newText = newText.Replace("{1/2}", "<i class=\"ms ms-1-2 ms-cost ms-shadow\" title=\"one-half generic mana\"></i>");
            newText = newText.Replace("{∞}", "<i class=\"ms ms-infinity ms-cost ms-shadow\" title=\"infinite generic mana\"></i>");
            newText = newText.Replace("{100}", "<i class=\"ms ms-100 ms-cost ms-shadow\" title=\"one hundred generic mana\"></i>");
            newText = newText.Replace("{1000000}", "<i class=\"ms ms-c ms-cost ms-shadow\" title=\"one million generic mana\"></i>");

            newText = newText.Replace("{S}", "<i class=\"ms ms-s ms-cost ms-shadow\" title=\"one snow mana\"></i>");

            for (var i = 1; i <= 20; i++)
            {
                newText = newText.Replace($"{{{i}}}", $"<i class=\"ms ms-{i} ms-cost ms-shadow\" title=\"{(i == 1 ? "one" : i.ToString())} generic mana\"></i>");
            }

            // TODO: phyrexian mana

            // Card symbols
            newText = newText.Replace("{T}", "<i class=\"ms ms-tap-alt ms-cost ms-shadow\" title=\"tap this permanent\"></i>");
            newText = newText.Replace("{U}", "<i class=\"ms ms-untap ms-cost ms-shadow\" title=\"untap this permanent\"></i>");
            newText = newText.Replace("{E}", "<i class=\"ms ms-e\" title=\"an energy counter\"></i>");

            return newText;
        }

        public static string RenderLineBreaks(string cardText)
        {
            return cardText?.Replace("\n", "<br />");
        }

        public static string RemoveReminderText(string cardText)
        {
            var startIndex = 0;
            var endIndex = 0;

            for (var i = 0; i < cardText.Length; i++)
            {
                if (cardText[i] == '(')
                {
                    startIndex = i;
                }

                if (cardText[i] == ')')
                {
                    endIndex = i;
                    break;
                }
            }

            if (startIndex > 0 && endIndex > 0)
            {
                return cardText.Remove(startIndex, endIndex - startIndex + 1);
            }

            return cardText;
        }

        public static string CardType(string typeLine)
        {
            var typeText = typeLine.Split('—')[0].Trim();
            return $"<i class=\"ms ms-{typeText.ReplaceSupertypes().ToLower().Trim()}\" title=\"{typeText}\"></i> " + typeText;
        }

        public static string RenderColorWheel(List<string> colors)
        {
            var pieHtml = $"\r\n<div class=\"pie pie-icon-size\" title=\"{FormatColor(colors)}\">\r\n";
            var offset = 0;
            int step;

            if (colors == null || colors.Count == 0)
            {
                // Each slice can only represent 50% of a wheel, so render the other half
                pieHtml += $"<div class=\"pie__segment\" style=\"--offset: 0; --value: 50; --bg: {WubrgHtmlColors["Colorless"]}\"></div>\r\n";
                pieHtml += $"<div class=\"pie__segment\" style=\"--offset: 50; --value: 50; --bg: {WubrgHtmlColors["Colorless"]}\"></div>\r\n";
                pieHtml += "</div>\r\n";

                return pieHtml;
            }

            var sortedColors = colors.OrderBy(i => WubrgOrder.IndexOf(i, StringComparison.Ordinal)).ToList();

            switch (sortedColors.Count)
            {
                default: // Each slice can only represent 50% of a wheel, so no need for cases 1 or 2 (special handling in place for case 1)
                    step = 50;
                    break;

                case 3:
                    step = 33;
                    break;

                case 4:
                    step = 25;
                    break;

                case 5:
                    step = 20;
                    break;
            }

            foreach (var color in sortedColors)
            {
                pieHtml += $"<div class=\"pie__segment\" style=\"--offset: {offset}; --value: {step}; --bg: {WubrgHtmlColors[FormatColor(color)]}\"></div>\r\n";
                offset += step;

                // Each slice can only represent 50% of a wheel, so render the other half
                if (colors.Count == 1)
                {
                    pieHtml +=
                        $"<div class=\"pie__segment\" style=\"--offset: {offset}; --value: {step}; --bg: {WubrgHtmlColors[FormatColor(color)]}\"></div>\r\n";
                }
            }

            pieHtml += "</div>\r\n";

            return pieHtml;
        }

        public static string ReplaceSupertypes(this string type)
        {
            return type.Replace("Legendary", string.Empty).Replace("Basic", string.Empty).Replace("Snow", string.Empty)
                .Replace("World", string.Empty).Replace("Host", string.Empty);
        }
    }
}
