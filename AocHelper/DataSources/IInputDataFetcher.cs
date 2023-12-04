namespace AocHelper.DataSources
{
    internal interface IInputDataFetcher
    {
        /// <summary>
        /// The day of the challenge
        /// </summary>
        int Day { get; }

        /// <summary>
        /// The year of the challenge
        /// </summary>
        int Year { get; }

        /// <summary>
        /// Try get the input data for the puzzle into 'data'
        /// </summary>
        /// <param name="data">The data for the puzzle</param>
        /// <returns>Whether the operation was a success</returns>
        bool GetInput(out string data);
    }
}
