using System.Diagnostics;
using System.Reflection;

using log4net;

namespace AocHelper
{
    public class ChallengeRunner 
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Stopwatch _sw = new Stopwatch();

        public static void RunChallenge<T>() where T : IAocChallenge, new()
        {
            _logger.InfoFormat("Creating challenge of type {0}", typeof(T).Name);
            Console.WriteLine($"Creating instance of {typeof(T).Name}");
            TimeSpan t = RunAction(() => new T(), out object day);
            var challenge = day as IAocChallenge;
            Console.WriteLine($"Time elapsed: {t}s\n");

            _logger.Info("Starting challenge 1");
            t = RunAction(challenge.Challenge1, out object part1);
            Console.WriteLine($"Day {challenge.Day} - Challenge 1: {part1}");
            Console.WriteLine($"Time elapsed: {t}s\n");

            try
            {
                _logger.Info("Starting challenge 2");
                t = RunAction(challenge.Challenge2, out object part2);
                Console.WriteLine($"Day {challenge.Day} - Challenge 2: {part2}");
                Console.WriteLine($"Time elapsed: {t}s\n");
            }
            catch (NotImplementedException)
            {
                _logger.Info("No challenge 2 implemented for this day, skipping");
            }
        }

        private static TimeSpan RunAction(Func<object> a, out object o)
        {
            _sw.Start();
            o = a();
            _sw.Stop();
            TimeSpan time = _sw.Elapsed;
            _sw.Reset();

            return time;
        }
    }
}
