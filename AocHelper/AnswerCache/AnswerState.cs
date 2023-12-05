using System.ComponentModel;

namespace AocHelper.AnswerCache
{
    public enum AnswerState
    {
        [Description("The given answer is too high")]
        TOO_HIGH,
        [Description("The given answer is too low")]
        TOO_LOW,
        [Description("The answer is close to the correct value, but not right")]
        CLOSE,
        [Description("The correct answer")]
        CORRECT,
        // For non-numeric answers
        [Description("The given answer is not correct")]
        INCORRECT,
    }
}
