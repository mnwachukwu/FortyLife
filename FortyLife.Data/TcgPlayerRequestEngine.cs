using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FortyLife.Data.TCGPlayer;

namespace FortyLife.Data
{
    public class TcgPlayerRequestEngine : RequestEngine
    {
        public enum ProductCategory
        {
            Null = 0,
            Mtg = 1
        }

        public const string ApiVersion = "v1.19.0";
        public const string PublicKey = "5E4802A2-D073-4B82-B3E9-6A1783F5A099";

        public string PrivateKey => string.Empty;

        public void AccessTokenRequest()
        {

        }

        public void StoreAccessToken()
        {

        }

        public string ReadAccessToken()
        {
            return string.Empty;
        }

        public int ProductIdRequest(string cardName, string setName)
        {
            return 0;
        }

        public MarketPriceResults CardPriceRequest(int productId)
        {
            return new MarketPriceResults();
        }
    }
}
