using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FortyLife.Data.Scryfall;
using Newtonsoft.Json;

namespace FortyLife.Data
{
    public class RequestEngine
    {
        // Returns JSON string
        internal string Get(string url)
        {
            // try to delay the request time by 200 ms, so that in perfect sequece we can only pull off 5 requests per second
            // scryfall will ban this IP if their endpoints are abused and they would like us to limit our requests to 10 per second
            Thread.Sleep(200); // TODO: better way to rate limit without shutting the thread down entirely

            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                        return JsonConvert.SerializeObject(new ScryfallList());

                    var reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response;
                using (var responseStream = errorResponse.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    var errorText = reader.ReadToEnd();
                    // log errorText
                }

                return JsonConvert.SerializeObject(new ScryfallList());
            }
        }

        // POST a JSON string
        internal void Post(string url, string jsonContent)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            var encoding = new System.Text.UTF8Encoding();
            var byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
            }
        }
    }
}
