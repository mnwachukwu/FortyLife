using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Core
{
    public static class StringExtensions
    {
        public static string GetNumber(this string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
