using AocHelper.Utilities;

namespace AocHelper.DataSources
{
    internal class WebRequestData : IInputDataFetcher
    {
        private const string URL = "https://adventofcode.com/{0}/day/{1}/input";

        public int Day { get; }
        public int Year { get; }

        public WebRequestData(int day, int year)
        {
            Day = day;
            Year = year;
        }

        /// <summary>
        /// Attempts to download the data from the advent of code website
        /// </summary>
        /// <param name="data">The output data</param>
        /// <returns></returns>
        public bool GetInput(out string data)
        {
            string url = string.Format(URL, Year, Day);
            Dictionary<string, string> headers = new() { ["Cookie"] = GetCookie() };

            var helper = new WebHelper();
            try
            {
                data = helper.Get(url, headers);
                return true;
            }
            catch (Exception ex)
            {
                data = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Gets the user cookie for AoC stored in 'token.txt' at the root of the sln
        /// </summary>
        /// <returns></returns>
        private static string GetCookie()
        {
            string cookieFile = Path.Combine(Constants.StartupPath, "token.txt");

            using StreamReader sr = new(cookieFile);
            string cookie = sr.ReadToEnd();

            return cookie;
        }
    }
}
