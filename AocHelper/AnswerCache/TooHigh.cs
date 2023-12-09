using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.AnswerCache
{
    /// <summary>
    /// Represents the behaviour for updating the record of 'Too High'
    /// </summary>
    internal class TooHigh : IAnswerState
    {
        /// <summary>
        /// The JSON key of this answer state
        /// </summary>
        public string Description => "Too High";

        /// <summary>
        /// How to add values
        /// </summary>
        public AnswerStateType AnswerType => AnswerStateType.SINGLE;

        /// <summary>
        /// Whether the value should be added / updated
        /// </summary>
        /// <param name="newValue">The new value to consider</param>
        /// <param name="existingValues">The existing smallest value deemed too high or Array.Empty if no such values registered</param>
        /// <returns></returns>
        public bool ShouldAddValue(string newValue, string[] existingValues)
        {
            return existingValues.Length != 0 && int.Parse(newValue) < int.Parse(existingValues[0]);
        }
    }
}
