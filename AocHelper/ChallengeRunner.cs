using System.Diagnostics;

using NLog;

namespace AocHelper
{
    public class ChallengeRunner 
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
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
            _logger.Info("Creating challenge of type {}", typeof(T).Name);
            notifier($"Creating instance of {typeof(T).Name}");
            TimeSpan t = RunAction(() => new T(), out object day);
            var challenge = (IAocChallenge)day;
            notifier($"   Time elapsed: {t}s");

            if (day is ITestAssert ta)
            {
                List<string> testResults = ta.Assert();
                if (testResults.Count == 0)
                    Console.WriteLine("All tests pass");
                else
                {
                    Console.WriteLine("The following tests failed:");
                    foreach (string testcase in testResults)
                        Console.WriteLine($"  - {testcase}");
                }
            }

            _logger.Info("Starting challenge 1");
            t = RunAction(challenge.Challenge1, out object part1);
            Console.WriteLine($"Challenge 1: {part1}");
            notifier($"   Time elapsed: {t}s");

            try
            {
                _logger.Info("Starting challenge 2");
                t = RunAction(challenge.Challenge2, out object part2);
                Console.WriteLine($"Challenge 2: {part2}");
                notifier($"   Time elapsed: {t}s");
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
