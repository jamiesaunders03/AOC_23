
namespace AocHelper
{
    internal class Constants
    {
        /// <summary>
        /// Input cache format path
        /// </summary>
        public const string INPUT_CACHE_PATH = ".cache/Input/{0}/day{1}.txt";

        /// <summary>
        /// Answers cache format path
        /// </summary>
        public const string ANSWERS_CACHE_PATH = ".cache/Answers/{0}/day{1}.json";

        /// <summary>
        /// Sln Folder
        /// </summary>
        public static readonly string StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
    }
}
