namespace CommonUtilities.FluentMethods
{
    public static class Extensions
    {
        public static Optional<TValue> ToOptional<TValue>(this TValue value)
            => Optional<TValue>.Create(value);

        public static Decomposer<TValue> Decompose<TValue>(this TValue value) 
            => new Decomposer<TValue>(value);

        public static Pipeline<TTarget> Pipe<TTarget>(this TTarget value)
            => new Pipeline<TTarget>(value);

        public static Parser Parser<TObject>(this TObject value)
            => new Parser();
    }
}