namespace AocHelper;

public interface ITestAssert
{
    /// <summary>
    /// Run asserts, returning the list of failing test cases, if any
    /// </summary>
    List<string> Assert();
}