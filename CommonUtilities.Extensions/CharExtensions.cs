namespace CommonUtilities.Extensions
{
    /// <summary>
    /// Provides extensions for the char type.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Repeats <paramref name="character"/> <paramref name="count"/> times.
        /// </summary>
        /// <param name="character">Symbol to repeat.</param>
        /// <param name="count">Amount of times to repeat <paramref name="character"/>.</param>
        /// <returns>String, that contains only <paramref name="character"/> symbol and it's length equal to
        /// <paramref name="count"/>
        /// </returns>
        public static string Repeat(this char character, int count) 
            => new string(character, count);
    }
}