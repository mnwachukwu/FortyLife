using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using FortyLife.DataAccess.TCGPlayer;
using Newtonsoft.Json;

namespace FortyLife.DataAccess
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
            setName = SanitizeSetName(setName);

            using (var db = new FortyLifeDbContext())
            {
                if (db.CardProductIds.Any(i => i.CardName == cardName && i.SetName == setName && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 7))
                {
                    return db.CardProductIds.First(i => i.CardName == cardName && i.SetName == setName).ProductId;
                }

                // Rate limit to be a good samaritan
                Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

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

                if (!string.IsNullOrEmpty(jsonResult))
                {
                    var productIdResult = JsonConvert.DeserializeObject<CategoryProductsResult>(jsonResult).Results
                        .OrderBy(i => i).FirstOrDefault();

                    if (productIdResult > 0)
                    {
                        var newProductId = new CardProductId
                        {
                            CardName = cardName,
                            SetName = setName,
                            ProductId = productIdResult,
                            CacheDate = DateTime.Now
                        };

                        db.CardProductIds.AddOrUpdate(newProductId);
                        db.SaveChanges();

                        return productIdResult;
                    }
                }

                return 0;
            }
        }

        public List<Price> CardPriceRequest(string cardName, string setName)
        {
            using (var db = new FortyLifeDbContext())
            {
                var productId = ProductIdRequest(cardName, setName);

                if (db.Prices.Any(i => i.ProductId == productId && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 1))
                {
                    return db.Prices.Where(i => i.ProductId == productId).ToList();
                }

                // Rate limit to be a good samaritan
                Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

                var jsonResult = Get($"{MarketPriceRequestUri}{productId}", ReadAccessToken());

                if (!string.IsNullOrEmpty(jsonResult))
                {
                    var prices = JsonConvert.DeserializeObject<MarketPriceResults>(jsonResult).Results;


                    if (prices != null)
                    {
                        foreach (var price in prices)
                        {
                            price.CacheDate = DateTime.Now;
                            db.Prices.AddOrUpdate(price);
                            db.SaveChanges();
                        }
                    }

                    return prices;
                }

                return null;
            }
        }

        public ProductDetail CardDetailsRequest(int productId)
        {
            using (var db = new FortyLifeDbContext())
            {
                if (db.ProductDetails.Any(i => i.ProductId == productId && DbFunctions.DiffDays(i.CacheDate, DateTime.Now) < 7))
                {
                    return db.ProductDetails.FirstOrDefault(i => i.ProductId == productId);
                }

                // Rate limit to be a good samaritan
                Thread.Sleep(200); // TODO: find a better way to do this without shutting down the thread

                var jsonResult = Get($"{ProductDetailsRequestUri}{productId}", ReadAccessToken());
                var productDetail = JsonConvert.DeserializeObject<ProductDetailsResults>(jsonResult).Results.First();

                if (productDetail != null)
                {
                    productDetail.CacheDate = DateTime.Now;
                    db.ProductDetails.AddOrUpdate(productDetail);
                    db.SaveChanges();
                }

                return productDetail;
            }
        }

        public string GetTcgPlayerUrl(string cardName, string setName)
        {
            var productId = ProductIdRequest(cardName, setName);
            var productDetailsResult = CardDetailsRequest(productId);

            return productDetailsResult != null ? productDetailsResult.Url : "#";
        }
    }
}
