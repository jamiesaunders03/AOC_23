using System.Diagnostics;
using System.Reflection;

using log4net;

namespace AocHelper
{
    public class ChallengeRunner 
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Stopwatch _sw = new();

        /// <summary>
        /// Runs the challenge for the given day, printing out the results and timings for the challenge.
        /// </summary>
        /// <typeparam name="T">The challenge to run</typeparam>
        public static void RunChallenge<T>() where T : IAocChallenge, new()
        {
            RunChallenge<T>(Console.WriteLine);
        }

        /// <summary>
        /// Runs the challenge for the given day, printing out the results and logging
        /// additional info to the given notifier
        /// </summary>
        /// <param name="notifier">Handles verbose info such as timing of challenges</param>
        /// <typeparam name="T">The challenge to run</typeparam>
        public static void RunChallenge<T>(Action<string> notifier) where T : IAocChallenge, new()
        {
            _logger.InfoFormat("Creating challenge of type {0}", typeof(T).Name);
            notifier($"Creating instance of {typeof(T).Name}");
            TimeSpan t = RunAction(() => new T(), out object day);
            var challenge = day as IAocChallenge;
            notifier($"Time elapsed: {t}s\n");

            _logger.Info("Starting challenge 1");
            t = RunAction(challenge.Challenge1, out object part1);
            Console.WriteLine($"Day {challenge.Day} - Challenge 1: {part1}");
            notifier($"Time elapsed: {t}s\n");

            try
            {
                _logger.Info("Starting challenge 2");
                t = RunAction(challenge.Challenge2, out object part2);
                Console.WriteLine($"Day {challenge.Day} - Challenge 2: {part2}");
                notifier($"Time elapsed: {t}s\n");
            }
            catch (NotImplementedException)
            {
                _logger.Info("No challenge 2 implemented for this day, skipping");
            }
            
            Console.WriteLine();
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
