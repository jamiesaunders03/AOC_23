using System.Reflection;

using log4net;

using AocHelper;
using AOC_24.Challenges;

log4net.Config.XmlConfigurator.Configure();

ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
logger.Info("Starting challenge runner");

const bool VERBOSE = true;
Action<string> timeReporter = VERBOSE ? Console.WriteLine : _ => { };

ChallengeRunner.RunChallenge<Day01>(timeReporter);
ChallengeRunner.RunChallenge<Day02>(timeReporter);
ChallengeRunner.RunChallenge<Day03>(timeReporter);
ChallengeRunner.RunChallenge<Day04>(timeReporter);
ChallengeRunner.RunChallenge<Day05>(timeReporter);
ChallengeRunner.RunChallenge<Day06>(timeReporter);
ChallengeRunner.RunChallenge<Day07>(timeReporter);
ChallengeRunner.RunChallenge<Day08>(timeReporter);
ChallengeRunner.RunChallenge<Day09>(timeReporter);
ChallengeRunner.RunChallenge<Day10>(timeReporter);
ChallengeRunner.RunChallenge<Day11>(timeReporter);
ChallengeRunner.RunChallenge<Day12>(timeReporter);
ChallengeRunner.RunChallenge<Day13>(timeReporter);
ChallengeRunner.RunChallenge<Day14>(timeReporter);
ChallengeRunner.RunChallenge<Day15>(timeReporter);
ChallengeRunner.RunChallenge<Day16>(timeReporter);
