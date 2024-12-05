using System.Reflection;

using log4net;

namespace AocHelper.DataSources
{
    internal class FileCacheSource : IInputDataFetcher
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The day of the challenge
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// The year of the challenge
        /// </summary>
        public int Year { get; }

        public FileCacheSource(int day, int year)
        {
            _logger.DebugFormat("Creating file loader instance for challenge {0}-12-{1}", day, year);

            Day = day;
            Year = year;
        }

        /// <summary>
        /// Try get the input data for the puzzle into 'data'
        /// </summary>
        /// <param name="data">The data for the puzzle</param>
        /// <returns>Whether the operation was a success</returns>
        public bool GetInput(out string data)
        {
            _logger.Info("Attempting to fetch data from text file");
            try
            {
                data = File.ReadAllText(GetFilePath());
                return true;
            }
            catch (FileNotFoundException)
            {
                _logger.Warn("No cached data could be found");
                data = "File does not exist, cannot load from cache";
                return false;
            }
            catch (Exception e)
            {
                _logger.Error("An unexpected error occurred: ", e);
                data = "Unexpected error: " + e.Message;
                return false;
            }
        }

        private string GetFilePath()
        {
            string fileRoute = Path.Join(Constants.StartupPath, Constants.INPUT_CACHE_PATH);
            fileRoute = string.Format(fileRoute, Year, Day);

            return fileRoute;
        }
    }
}
