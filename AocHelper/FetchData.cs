using AocHelper.DataSources;
using log4net;
using System.Reflection;

namespace AocHelper
{
    public class FetchData
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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
        /// <exception cref="IOException">Throws IO exception if the data cannot be read from any source</exception>
        public string ReadInput()
        {
            _logger.Info("Fetching data");
            foreach (IInputDataFetcher source in _dataSources)
            {
                bool result = source.GetInput(out string output);
                if (result)
                {
                    InputSave save = new(Year, Day);
                    save.Save(output);
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
                new FileCacheSource(Day, Year),
                new WebRequestData(Day, Year),
            };
        }
    }
}
