
namespace AocHelper.AnswerCache
{
    /// <summary>
    /// Models an incorrect answer
    /// </summary>
    internal class Incorrect : IAnswerState
    {
        /// <summary>
        /// The JSON key of this answer state
        /// </summary>
        public string Description => "Incorrect";

        /// <summary>
        /// How to add values
        /// </summary>
        public AnswerStateType AnswerType => AnswerStateType.ADDITIVE;

        /// <summary>
        /// Whether the value should be added / updated
        /// </summary>
        /// <param name="newValue">The new value to consider</param>
        /// <param name="existingValues">The existing value(s)</param>
        /// <returns></returns>
        public bool ShouldAddValue(string newValue, string[] existingValues)
        {
            return !existingValues.Contains(newValue);
        }
    }
}
