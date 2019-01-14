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
                return string.Empty;
            
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
    }
}
