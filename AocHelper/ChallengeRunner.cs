
namespace AocHelper
{
    public class ChallengeRunner 
    {
        public static void RunChallenge<T>() where T : IAocChallenge, new()
        {
            IAocChallenge challenge = new T();

            string part1 = challenge.Challenge1();
            Console.WriteLine($"Day {challenge.Day} - Challenge 1: {part1}");

            try
            {
                string part2 = challenge.Challenge2();
                Console.WriteLine($"Day {challenge.Day} - Challenge 2: {part2}");
            }
            catch (NotImplementedException) {}
        }
    }
}
