using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper
{
    internal class InputSave
    {
        private const string CACHE_PATH = ".cache/Input/{0}/";
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Year { get; }
        public int Day { get; }

        public InputSave(int year, int day)
        {
            Year = year;
            Day = day;
        }

        /// <summary>
        /// Caches input to file
        /// </summary>
        /// <param name="data">The data to save</param>
        /// <param name="force">Whether to force overwrite existing data</param>
        public void Save(string data, bool force = false)
        {
            _logger.Info("Saving data cache");

            string dirPath = GetFilePath();
            string fileName = $"day{Day}.txt";
            string filePath = Path.Combine(dirPath, fileName);

            if (!force && File.Exists(filePath))
            {
                _logger.Info("Data cache already exists, no save required");
                return;
            }

            Directory.CreateDirectory(dirPath);
            File.WriteAllText(filePath, data);
        }

        private string GetFilePath()
        {
            string fileRoute = Path.Join(Constants.StartupPath, CACHE_PATH);
            fileRoute = string.Format(fileRoute, Year, Day);

            return fileRoute;
        }
    }
}
