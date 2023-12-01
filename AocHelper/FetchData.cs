
using AocHelper.DataSources;

namespace AocHelper
{
    public class FetchData
    {
        private IInputDataFetcher[] _dataSources = Array.Empty<IInputDataFetcher>();

        public int Day { get; }
        public int Year { get; }

        public FetchData(int day, int year = 2023)
        {
            Day = day;
            Year = year;

            SetDataSources();
        }

        /// <summary>
        /// Reads the input data for the current days challenge
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException">Throws IO exception if the data cannot be read</exception>
        public string ReadInput()
        {
            foreach (IInputDataFetcher source in _dataSources)
            {
                bool result = source.GetInput(out string output);
                if (result)
                {
                    return output;
                }

                Console.Error.WriteLine(output);
            }

            throw new IOException("Unable to get input for challenge");
        }

        private void SetDataSources()
        {
            _dataSources = new IInputDataFetcher[]
            {
                new WebRequestData(Day, Year),
            };
        }
    }
}
