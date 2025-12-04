using System.Reflection;

using log4net;

using AocHelper;
using AOC_25.Challenges;

log4net.Config.XmlConfigurator.Configure();

ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
logger.Info("Starting challenge runner");

const bool VERBOSE = true;
Action<string> timeReporter = VERBOSE ? Console.WriteLine : _ => { };

ChallengeRunner.RunChallenge<Day01>(timeReporter);
ChallengeRunner.RunChallenge<Day02>(timeReporter);
ChallengeRunner.RunChallenge<Day04>(timeReporter);
