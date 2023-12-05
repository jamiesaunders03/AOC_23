using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.AnswerCache
{
    internal class StoreAnswer
    {
        /// <summary>
        /// Saves an answer to the cache
        /// </summary>
        /// <param name="answer">The answer to save</param>
        /// <param name="state">The given state of the answer</param>
        /// <param name="day">The day of challenge</param>
        /// <param name="year">The year of challenge</param>
        /// <returns>Whether a change was made to the file</returns>
        public static bool SaveAnswer(string answer, AnswerState state, int day, int year)
        {
            return false;
        }

        private static string GetFilePath(int year, int day)
        {
            return "";
        }
    }
}
