using AocHelper;
using AOC_25.Challenges;
using NLog;

Logger logger = LogManager.GetCurrentClassLogger();;
logger.Info("Starting challenge runner");

const bool VERBOSE = false;
Action<string> timeReporter = VERBOSE ? Console.WriteLine : _ => { };

ChallengeRunner.RunChallenge<Day01>(timeReporter);
ChallengeRunner.RunChallenge<Day02>(timeReporter);
ChallengeRunner.RunChallenge<Day03>(timeReporter);
ChallengeRunner.RunChallenge<Day04>(timeReporter);
ChallengeRunner.RunChallenge<Day05>(timeReporter);
ChallengeRunner.RunChallenge<Day06>(timeReporter);
