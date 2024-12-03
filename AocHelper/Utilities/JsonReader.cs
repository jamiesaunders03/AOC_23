using System.Text.Json;

namespace AocHelper.Utilities
{
    internal class JsonReader
    {
        public static Dictionary<string, List<string>> ReadFile(string path)
        {
            using StreamReader file = File.OpenText(path);
            string content = file.ReadToEnd();

            if (content == "")
                return new Dictionary<string, List<string>>();
            
            var json = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(content);
            if (json is null)
                throw new InvalidDataException("Data in wrong format to de-serialize");
            
            return json;
        }
    }
}
