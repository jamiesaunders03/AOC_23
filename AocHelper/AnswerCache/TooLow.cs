using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.AnswerCache
{
    /// <summary>
    /// Represents the behaviour for updating the record of 'Too Low'
    /// </summary>
    internal class TooLow : IAnswerState
    {
        /// <summary>
        /// The JSON key of this answer state
        /// </summary>
        public string Description => "Too Low";

        /// <summary>
        /// How to add values
        /// </summary>
        public AnswerStateType AnswerType => AnswerStateType.SINGLE;

        /// <summary>
        /// Whether the value should be added / updated
        /// </summary>
        /// <param name="newValue">The new value to consider</param>
        /// <param name="existingValues">The existing highest value deemed too small or Array.Empty if no such values registered</param>
        /// <returns></returns>
        public bool ShouldAddValue(string newValue, string[] existingValues)
        {
            return existingValues.Length != 0 && int.Parse(newValue) > int.Parse(existingValues[0]);
        }
    }
}
