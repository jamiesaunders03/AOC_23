using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.DataSources
{
    internal class FileCacheSource : IInputDataFetcher
    {
        private const string CACHE_PATH = ".cache/Input/{0}/day{1}.txt";

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
            try
            {
                data = File.ReadAllText(GetFilePath());
                return true;
            }
            catch (FileNotFoundException)
            {
                data = "File does not exist, cannot load from cache";
                return false;
            }
            catch (Exception e)
            {
                data = "Unexpected error: " + e.Message;
                return false;
            }
        }

        private string GetFilePath()
        {
            string fileRoute = Path.Join(Constants.StartupPath, CACHE_PATH);
            fileRoute = string.Format(fileRoute, Year, Day);

            return fileRoute;
        }
    }
}
