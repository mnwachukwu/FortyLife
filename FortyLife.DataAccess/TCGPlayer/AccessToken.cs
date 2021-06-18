using System;

namespace FortyLife.DataAccess.TCGPlayer
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
