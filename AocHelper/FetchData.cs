namespace AocHelper
{
    public class FetchData
    {
        private const string URL = "https://adventofcode.com/{0}/day/{1}/input";

        public int Day { get; }
        public int Year { get; }

        public FetchData(int day, int year = 2023)
        {
            Day = day;
            Year = year;
        }

        public string ReadInput()
        {
            string url = string.Format(URL, Year, Day);
            Dictionary<string, string> headers = new() { ["Cookie"] = GetCookie()};

            var helper = new WebHelper();
            string output = helper.Get(url, headers);

            return output;
        }

        public string GetCookie()
        {
            const string cookieFile = "../../../../token.txt";

            using StreamReader sr = new StreamReader(cookieFile);
            string cookie = sr.ReadToEnd();

            return cookie;
        }
    }
}