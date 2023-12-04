using System.Net;

namespace AocHelper.Utilities
{
    internal class WebHelper
    {
        /// <summary>
        /// Make a get request to a given url with the supplied headers and return the response as a string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public string Get(string uri, Dictionary<string, string> headers)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            foreach (KeyValuePair<string, string> header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            using var response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
