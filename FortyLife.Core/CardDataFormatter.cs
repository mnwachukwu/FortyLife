using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Core
{
    public static class CardDataFormatter
    {
        public static string FormatColor(List<string> colors)
        {
            if (colors == null || colors.Count == 0)
                return "Colorless";
            
            var sb = new StringBuilder();

            foreach (var color in colors)
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
            return $"<i class=\"ms ms-{typeText.Replace("Legendary", string.Empty).ToLower().Trim()}\" title=\"{typeText}\"></i> " + typeText;
        }

        public static string RenderColorWheel(List<string> colors)
        {
            return
                $"\r\n<div class=\"pie pie-icon-size\" title=\"{FormatColor(colors)}\">\r\n " +
                "<div class=\"pie__segment\" style=\"--offset: 0; --value: 20; --bg: #ffff99;\"></div>\r\n" + // White
                "<div class=\"pie__segment\" style=\"--offset: 20; --value: 20; --bg: #3366ff;\"></div>\r\n" + // Blue
                "<div class=\"pie__segment\" style=\"--offset: 40; --value: 20; --bg: #6600ff;\"></div>\r\n" + // Black
                "<div class=\"pie__segment\" style=\"--offset: 60; --value: 20; --bg: #ff5050;\"></div>\r\n" + // Red
                "<div class=\"pie__segment\" style=\"--offset: 80; --value: 20; --bg: #33cc33;\"></div>\r\n" + // Green
                "</div>\r\n";
        }
    }
}
