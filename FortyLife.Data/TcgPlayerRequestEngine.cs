using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FortyLife.Data.TCGPlayer;
using Newtonsoft.Json;

namespace FortyLife.Data
{
    public class TcgPlayerRequestEngine : RequestEngine
    {
        public enum ProductCategory
        {
            Null = 0,
            Mtg = 1
        }

        private static readonly string AppDataPath = $"{HttpRuntime.AppDomainAppPath}App_Data\\";
        private const string TokenUri = "https://api.tcgplayer.com/token";
        private readonly string CategoryProductSearchUri = $"http://api.tcgplayer.com/{ApiVersion}/catalog/categories/1/search";
        private readonly string MarketPriceRequestUri = $"http://api.tcgplayer.com/{ApiVersion}/pricing/product/";
        private readonly string ProductDetailsRequestUri = $"https://api.tcgplayer.com/{ApiVersion}/catalog/products/";
        private const string ApiVersion = "v1.19.0";
        private const string PublicKey = "5E4802A2-D073-4B82-B3E9-6A1783F5A099";

        private static string PrivateKey => File.ReadAllText(AppDataPath + "private-key.txt");

        /// <summary>
        /// Authenticate the application and return the resultant token.
        /// </summary>
        /// <returns></returns>
        private TcgPlayerAccessToken AccessTokenRequest()
        {
            // Rate limit to be a good samaritan
            Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

            var body = $"grant_type=client_credentials&client_id={PublicKey}&client_secret={PrivateKey}";
            var jsonResult = Post(TokenUri, body, RequestBodyType.PlainText) // get the json token and sanitize it for newtonsoft deserialization
                .Replace("access_token", "accessToken")
                .Replace("token_type", "tokenType")
                .Replace("expires_in", "expiresIn")
                .Replace(".issued", "issued")
                .Replace(".expires", "expires");

            return JsonConvert.DeserializeObject<TcgPlayerAccessToken>(jsonResult);
        }

        private static void StoreAccessToken(TcgPlayerAccessToken token)
        {
            var filePath = AppDataPath + "access-token.txt";

            // overwrite what's there
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(token, Formatting.Indented));
        }

        private string ReadAccessToken()
        {
            var jsonText = File.ReadAllText(AppDataPath + "access-token.txt");
            var accessToken = JsonConvert.DeserializeObject<TcgPlayerAccessToken>(jsonText);

            // our token is expired
            if (DateTime.Now >= accessToken.Expires)
            {
                StoreAccessToken(AccessTokenRequest());

                // re-read the token
                jsonText = File.ReadAllText(AppDataPath + "access-token.txt");
                accessToken = JsonConvert.DeserializeObject<TcgPlayerAccessToken>(jsonText);
            }

            // return the actual token string
            return accessToken.AccessToken;
        }

        /// <summary>
        /// Scryfall and TCG Player don't agree on some set names, so this method will help us sanitize Scryfall set names to work with TCG Player's API.
        /// </summary>
        /// <param name="setName"></param>
        /// <returns></returns>
        private static string SanitizeSetName(string setName)
        {
            // TODO: the method, lol - but seriously, we'll need to figure out where the discrepencies are and .Replace() where needed
            return setName
                .Replace("Time Spiral Timeshifted", "Timeshifted");
        }

        public int ProductIdRequest(string cardName, string setName)
        {
            // Rate limit to be a good samaritan
            Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

            setName = SanitizeSetName(setName);

            // when searching for products in the TCG Player API, it only accepts the name of the front face of double faced cards
            if (cardName.Contains("//"))
            {
                cardName = cardName.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }

            var searchCriteria = new CategoryProductsSearchBody
            {
                Sort = "name",
                Limit = 5,
                Offset = 0,
                Filters = new List<Filter>
                {
                    new Filter
                    {
                        Name = Filter.FilterName.ProductName.ToString(),
                        Values = new List<string> {cardName}
                    },
                    new Filter
                    {
                        Name = Filter.FilterName.SetName.ToString(),
                        Values = new List<string> {setName}
                    }
                }
            };

            var body = JsonConvert.SerializeObject(searchCriteria);
            var jsonResult = Post(CategoryProductSearchUri, body, RequestBodyType.Json, ReadAccessToken());

            return JsonConvert.DeserializeObject<CategoryProductsResult>(jsonResult).Results.FirstOrDefault();
        }

        public MarketPriceResults CardPriceRequest(string cardName, string setName)
        {
            // Rate limit to be a good samaritan
            Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

            var productId = ProductIdRequest(cardName, setName);
            var jsonResult = Get($"{MarketPriceRequestUri}{productId}", ReadAccessToken());
            return JsonConvert.DeserializeObject<MarketPriceResults>(jsonResult);
        }

        public ProductDetailsResults CardDetailsRequest(int productId)
        {
            // Rate limit to be a good samaritan
            Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

            var jsonResult = Get($"{ProductDetailsRequestUri}{productId}", ReadAccessToken());
            return JsonConvert.DeserializeObject<ProductDetailsResults>(jsonResult);
        }

        public string GetTcgPlayerUrl(string cardName, string setName)
        {
            var productId = ProductIdRequest(cardName, setName);
            var productDetailsResults = CardDetailsRequest(productId).Results;

            return productDetailsResults != null ? productDetailsResults.FirstOrDefault()?.Url : "#";
        }
    }
}
