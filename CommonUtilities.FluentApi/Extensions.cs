namespace CommonUtilities.FluentApi
{
    /// <summary>
    /// Extensions for the Fluent API
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Creates pipeline for any object.
        /// </summary>
        /// <param name="target">Object that pipeline starts with.</param>
        /// <typeparam name="TTarget">Type of the object that will be used as init pipeline state.</typeparam>
        /// <returns>Pipeline with the <paramref name="target"/> object.</returns>
        public static Pipe<TTarget> Pipe<TTarget>(this TTarget target) 
            => new Pipe<TTarget>(target);
    }
}