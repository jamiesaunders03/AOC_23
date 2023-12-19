using System.Reflection;

using log4net;

using AocHelper;
using AOC_23.Challenges;

log4net.Config.XmlConfigurator.Configure();

ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
logger.Info("Starting challenge runner");

ChallengeRunner.RunChallenge<Day17>();
