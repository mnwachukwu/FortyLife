using System.IO;
using System.Net;

namespace FortyLife.Data
{
    public class RequestEngine
    {
        // Returns JSON string
        internal string Get(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
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
                throw;
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
