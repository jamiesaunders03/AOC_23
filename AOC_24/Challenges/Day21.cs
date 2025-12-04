using AocHelper;

namespace AOC_24.Challenges;

internal class Day21 : IAocChallenge
{
    public int Day => 21;

    public Day21()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
    }

    public string Challenge1()
    {
        throw new NotImplementedException();
    }

    public string Challenge2()
    {
        throw new NotImplementedException();
    }

    #region Keypads

    private interface IKeypad
    {
        /// <summary>
        /// Given a target set of keys to press, returns the keys that need to be pressed on the current keypad
        /// to create that output.
        /// </summary>
        /// <param name="keys">The keys that are wanting to be pressed</param>
        /// <param name="startPos">The initial position of the pointer</param>
        /// <returns></returns>
        string[] GetCodes(string keys, int startPos);
    }

    #endregion
}