using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.Data.TCGPlayer
{
    public class TcgPlayerAccessToken
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public long ExpiresIn { get; set; }

        public string UserName { get; set; }

        public DateTime Issued { get; set; }

        public DateTime Expires { get; set; }
    }
}
