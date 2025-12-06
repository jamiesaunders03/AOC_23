using AocHelper;
using AOC_23.Challenges;
using NLog;

Logger logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting challenge runner");

ChallengeRunner.RunChallenge<Day19>();
