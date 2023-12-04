
using log4net;
using System.Reflection;

namespace AocHelper
{
    public class ChallengeRunner 
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void RunChallenge<T>() where T : IAocChallenge, new()
        {
            _logger.InfoFormat("Creating challenge of type {0}", typeof(T).Name);
            IAocChallenge challenge = new T();

            _logger.Info("Starting challenge 1");
            string part1 = challenge.Challenge1();
            Console.WriteLine($"Day {challenge.Day} - Challenge 1: {part1}");

            try
            {
                _logger.Info("Starting challenge 2");
                string part2 = challenge.Challenge2();
                Console.WriteLine($"Day {challenge.Day} - Challenge 2: {part2}");
            }
            catch (NotImplementedException)
            {
                _logger.Info("No challenge 2 implemented for this day, skipping");
            }
        }
    }
}
