using System.ComponentModel;

namespace AocHelper.AnswerCache
{
    public enum AnswerStateType
    {
        [Description("Multiple values can be used")]
        ADDITIVE,
        [Description("Only 1 value")]
        SINGLE,
    }

    public interface IAnswerState
    {
        /// <summary>
        /// The JSON key of this answer state
        /// </summary>
        string Description { get; }

        /// <summary>
        /// How to add values
        /// </summary>
        AnswerStateType AnswerType { get; }

        /// <summary>
        /// Whether the value should be added / updated
        /// </summary>
        /// <param name="newValue">The new value to consider</param>
        /// <param name="existingValues">The existing value(s)</param>
        /// <returns></returns>
        public bool ShouldAddValue(string newValue, string[] existingValues);
    }
}
