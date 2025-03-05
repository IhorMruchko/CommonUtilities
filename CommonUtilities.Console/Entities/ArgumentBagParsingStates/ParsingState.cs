namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal abstract class ParsingState
    {
        internal ArgumentBag Context { get; set; }

        internal abstract int Parse(string[] arguments, int currentArgumentIndex);
    }
}