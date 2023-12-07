using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AocHelper.Utilities
{
    internal class JsonReader
    {
        public static dynamic ReadFile(string path)
        {
            using StreamReader file = File.OpenText(path);
            var json = JObject.Parse(file.ReadToEnd());
            return json;
        }
    }
}
