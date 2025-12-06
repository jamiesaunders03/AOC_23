using AocHelper;
using AOC_24.Challenges;
using NLog;

Logger logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting challenge runner");

const bool VERBOSE = true;
Action<string> timeReporter = VERBOSE ? Console.WriteLine : _ => { };

// ChallengeRunner.RunChallenge<Day01>(timeReporter);
// ChallengeRunner.RunChallenge<Day02>(timeReporter);
// ChallengeRunner.RunChallenge<Day03>(timeReporter);
// ChallengeRunner.RunChallenge<Day04>(timeReporter);
// ChallengeRunner.RunChallenge<Day05>(timeReporter);
// ChallengeRunner.RunChallenge<Day06>(timeReporter);
// ChallengeRunner.RunChallenge<Day07>(timeReporter);
// ChallengeRunner.RunChallenge<Day08>(timeReporter);
// ChallengeRunner.RunChallenge<Day09>(timeReporter);
// ChallengeRunner.RunChallenge<Day10>(timeReporter);
// ChallengeRunner.RunChallenge<Day11>(timeReporter);
// ChallengeRunner.RunChallenge<Day12>(timeReporter);
// ChallengeRunner.RunChallenge<Day13>(timeReporter);
// ChallengeRunner.RunChallenge<Day14>(timeReporter);
// ChallengeRunner.RunChallenge<Day15>(timeReporter);
// ChallengeRunner.RunChallenge<Day16>(timeReporter);
ChallengeRunner.RunChallenge<Day17>(timeReporter);
// ChallengeRunner.RunChallenge<Day18>(timeReporter);
// ChallengeRunner.RunChallenge<Day19>(timeReporter);
// ChallengeRunner.RunChallenge<Day20>(timeReporter);
ChallengeRunner.RunChallenge<Day21>(timeReporter);
