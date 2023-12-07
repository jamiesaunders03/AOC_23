
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonReader = AocHelper.Utilities.JsonReader;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AocHelper.AnswerCache
{
    internal class StoreAnswer
    {
        /// <summary>
        /// Saves an answer to the cache
        /// </summary>
        /// <param name="answer">The answer to save</param>
        /// <param name="state">The given state of the answer</param>
        /// <param name="day">The day of challenge</param>
        /// <param name="year">The year of challenge</param>
        /// <returns>Whether a change was made to the file</returns>
        public static bool SaveAnswer(string answer, IAnswerState state, int day, int year)
        {
            string path = GetFilePath(year, day);
            CreateDir(path);

            dynamic json = JsonReader.ReadFile(path);
            string[] current = json[state.Description];
            if (current != null && !state.ShouldAddValue(answer, current))
                return false;

            current ??= Array.Empty<string>();
            switch (state.AnswerType)
            {
                case AnswerStateType.ADDITIVE:
                    json[state.Description] = new List<string>(current) { answer };
                    break;
                case AnswerStateType.SINGLE:
                    json[state.Description] = answer;
                    break;
                default:
                    throw new NotImplementedException("Unknown Answer State");
            }

            string jsonString = JsonSerializer.Serialize(json);
            File.WriteAllText(path, jsonString);

            return true;
        }

        private static string GetFilePath(int year, int day)
        {
            string path = Path.Combine(Constants.StartupPath, Constants.ANSWERS_CACHE_PATH);
            return string.Format(path, year, day);
        }

        private static void CreateDir(string path)
        {
            string[] parts = path.Split('/');
            string dir = string.Join("\\", parts.SkipLast(1));

            try
            {
                Directory.CreateDirectory(dir);
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
            }
            catch (IOException) { }
        }
    }
}
