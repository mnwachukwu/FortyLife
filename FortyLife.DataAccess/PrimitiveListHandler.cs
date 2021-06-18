using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess
{
    public static class PrimitiveListHandler
    {
        public static string ToString(List<string> list)
        {
            var builder = new StringBuilder();

            foreach (var item in list)
            {
                builder.Append($"{item},");
            }

            return builder.Length > 0 ? builder.ToString().Substring(0, builder.Length - 1) : string.Empty;
        }
    }
}
