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
        internal enum RequestBodyType
        {
            None,
            PlainText,
            Json
        }

        // Returns JSON string
        internal string Get(string url, string accessToken = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            try
            {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                        return "{ }"; // return an empty object

                    var reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response;
                using (var errorResponseStream = errorResponse.GetResponseStream())
                {
                    var reader = new StreamReader(errorResponseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    var errorText = reader.ReadToEnd();
                    // log errorText
                }

                return string.Empty;
            }
        }

        // POST a content string and get a response string
        internal string Post(string url, string body, RequestBodyType bodyType, string accessToken = "")
        {
            var request = GenerateRequestHeader(url, bodyType, accessToken);
            var encoding = new System.Text.UTF8Encoding();
            var byteArray = encoding.GetBytes(body);
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream == null)
                        return "{ }"; // return an empty object

                    var reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response;
                using (var errorResponseStream = errorResponse.GetResponseStream())
                {
                    var reader = new StreamReader(errorResponseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    var errorText = reader.ReadToEnd();
                    // log errorText
                }

                return string.Empty;
            }
        }

        private static HttpWebRequest GenerateRequestHeader(string url, RequestBodyType bodyType, string accessToken = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.Accept = @"application/json";

            switch (bodyType)
            {
                case RequestBodyType.Json:
                    request.ContentType = @"application/json";
                    break;

                case RequestBodyType.PlainText:
                    request.ContentType = @"text/plain";
                    break;
            }

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return request;
        }
    }
}
