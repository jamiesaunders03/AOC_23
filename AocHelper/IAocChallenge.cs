
namespace AocHelper
{
    public interface IAocChallenge
    {
        int Day { get; }
        string Challenge1();
        string Challenge2();
    }
}
