namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class CompletedParsingState : ParsingState
    {
        internal override int Parse(string[] arguments, int currentArgumentIndex) 
            => arguments.Length;
    }
}