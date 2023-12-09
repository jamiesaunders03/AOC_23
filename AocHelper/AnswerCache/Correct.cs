using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.AnswerCache
{
    /// <summary>
    /// Represents the behaviour for updating the 'Correct' value
    /// </summary>
    internal class Correct : IAnswerState
    {
        /// <summary>
        /// The JSON key of this answer state
        /// </summary>
        public string Description => "Correct";

        /// <summary>
        /// How to add values
        /// </summary>
        public AnswerStateType AnswerType => AnswerStateType.SINGLE;

        /// <summary>
        /// Whether the value should be added / updated
        /// </summary>
        /// <param name="newValue">The new value to consider</param>
        /// <param name="existingValues">The correct value if stored, else Array.Empty</param>
        /// <returns></returns>
        public bool ShouldAddValue(string newValue, string[] existingValues)
        {
            return existingValues.Length == 0;
        }
    }
}
