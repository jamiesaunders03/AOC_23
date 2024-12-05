using System.Text.Json;

namespace AocHelper.Utilities
{
    internal static class JsonReader
    {
        /// <summary>
        /// Reads the file at the given location as a structure representing past attempts
        /// </summary>
        /// <param name="path">The file path</param>
        /// <returns>Representation of previous attempts</returns>
        /// <exception cref="InvalidDataException">If the data in the file does not correctly deserialize</exception>
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
