using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper
{
    internal class WebHelper
    {
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
